import axios from 'axios'

const API_URL = 'http://localhost:5100/api/verification'

// 發送註冊驗證碼
export const sendRegisterVerificationCode = async (email) => {
    try {
        const response = await axios.post(`${API_URL}/register`, { email })
        return response.data
    }
    catch (error) {
        throw error
    }
}

// 發送重設密碼驗證碼
export const sendResetVerificationCode = async (email) => {
    try {
        const response = await axios.post(`${API_URL}/reset`, { email })
        return response.data
    }
    catch (error) {
        throw error
    }
}