import axios from 'axios'

const API_URL = 'http://localhost:5100/api/register'

// 送出註冊資料
export const register = async (payload) => {
    try {
        const response = await axios.post(API_URL, payload)
        return response.data
    }
    catch (error) {
        throw error
    }
}