import axios from 'axios'

const API_URL = 'http://localhost:5100/api/excel'
const getAuthHeaders = () => ({
    headers: {
        Authorization: `Bearer ${localStorage.getItem('token')}`,
    }
})

// 匯出收支紀錄 Excel
export const exportRecordsToExcel = async ({ accountId = null, categoryId = null, type = null, startDate = null, endDate = null }) => {
    const params = new URLSearchParams();

    if (accountId) {
        params.append('accountId', accountId);
    }
    if (categoryId) {
        params.append('categoryId', categoryId);
    }
    if (type) {
        params.append('type', type);
    }
    if (startDate) {
        params.append('startDate', new Date(startDate).toISOString());
    }
    if (endDate) {
        params.append('endDate', new Date(endDate).toISOString());
    }

    const response = await axios.get(`${API_URL}?${params.toString()}`, {
        ...getAuthHeaders(),
        responseType: 'blob',
    });
    return response.data;
}