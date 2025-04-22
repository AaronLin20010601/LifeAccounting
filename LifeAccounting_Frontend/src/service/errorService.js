export default {
    handleError(error) {
        let errorMessage = '';
    
        if (error.response) {
            if (error.response.data && error.response.data.errors) {
                // 處理字段驗證錯誤
                const errors = error.response.data.errors;
                errorMessage = '';
                for (const key in errors) {
                    if (errors.hasOwnProperty(key)) {
                        errorMessage += `${errors[key].join(', ')}\n`;
                    }
                }
            } else if (error.response.data && error.response.data.message) {
                // 後端錯誤訊息
                errorMessage = error.response.data.message;
            } else {
                // 預設錯誤訊息
                errorMessage = 'An error occurred.';
            }
        } else {
            // 其他錯誤
            errorMessage = error.message || 'An error occurred.';
        }
    
        return errorMessage;
    }
}