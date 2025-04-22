import axios from 'axios'

const API_URL = 'http://localhost:5100/api/reset'

// 送出重設密碼
export const reset = async (payload) => {
    try {
        const response = await axios.post(API_URL, payload)
        return response.data
    }
    catch (error) {
        throw error
    }
}