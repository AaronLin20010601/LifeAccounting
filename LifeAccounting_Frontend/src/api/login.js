import axios from 'axios'

const API_URL = 'http://localhost:5100/api/login'

// 發送登入請求到後端並獲取 JWT
export const login = async (email, password) => {
    try {
        const response = await axios.post(API_URL, { email, password })
        return response.data
    }
    catch (error) {
        throw error
    }
}