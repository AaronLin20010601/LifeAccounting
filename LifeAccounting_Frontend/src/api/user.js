import axios from 'axios'

const API_URL = 'http://localhost:5100/api/user'
const getAuthHeaders = () => ({
    headers: {
        Authorization: `Bearer ${localStorage.getItem('token')}`,
    }
})

// 取得使用者資訊
export const fetchUser = async() => {
    const response = await axios.get(API_URL, getAuthHeaders())
    return response.data
}

// 更新帳戶和紀錄同步
export const toggleSync = async(isSync) => {
    const response = await axios.patch(API_URL, { isSync }, getAuthHeaders())
    return response.data
}