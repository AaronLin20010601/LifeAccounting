import axios from 'axios'

const API_URL = 'http://localhost:5100/api/account'
const getAuthHeaders = () => ({
    headers: {
        Authorization: `Bearer ${localStorage.getItem('token')}`,
    }
})

// 取得帳戶列表
export const fetchAccounts = async() => {
    const response = await axios.get(API_URL, getAuthHeaders())
    return response.data
}

// 帳戶餘額轉移
export const transferBalance = async(form) => {
    const response = await axios.patch(API_URL, form, getAuthHeaders())
    return response.data
}

// 新增帳戶
export const createAccount = async (account) => {
    try {
        const response = await axios.post(API_URL, account, getAuthHeaders())
        return response.data
    } 
    catch (error) {
        throw error
    }
}

// 取得待編輯的帳戶
export const getEditAccount = async (id) => {
    try {
        const response = await axios.get(`${API_URL}/${id}`, getAuthHeaders())
        return response.data
    } 
    catch (error) {
        throw error
    }
}

// 更新帳戶
export const updateAccount = async (id, account) => {
    try {
        const response = await axios.patch(`${API_URL}/${id}`, account, getAuthHeaders())
        return response.data
    } 
    catch (error) {
        throw error
    }
}

// 刪除帳戶
export const deleteAccount = async (id) => {
    try {
        const response = await axios.delete(`${API_URL}/${id}`, getAuthHeaders())
        return response.data
    } 
    catch (error) {
        throw error
    }
}