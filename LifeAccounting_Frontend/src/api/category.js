import axios from 'axios'

const API_URL = 'http://localhost:5100/api/category'
const getAuthHeaders = () => ({
    headers: {
        Authorization: `Bearer ${localStorage.getItem('token')}`,
    }
})

// 取得收支類型列表
export const fetchCategories = async () => {
    const response = await axios.get(API_URL, getAuthHeaders())
    return response.data
}

// 新增收支類型
export const createCategory = async (category) => {
    try {
        const response = await axios.post(API_URL, category, getAuthHeaders())
        return response.data
    } 
    catch (error) {
        throw error
    }
}

// 取得要編輯的收支類型
export const getEditCategory = async (id) => {
    try {
        const response = await axios.get(`${API_URL}/${id}`, getAuthHeaders())
        return response.data
    } 
    catch (error) {
        throw error
    }
}

// 更新收支類型
export const updateCategory = async (id, category) => {
    try {
        const response = await axios.patch(`${API_URL}/${id}`, category, getAuthHeaders())
        return response.data
    } 
    catch (error) {
        throw error
    }
}

// 刪除收支類型
export const deleteCategory = async (id) => {
    try {
        const response = await axios.delete(`${API_URL}/${id}`, getAuthHeaders())
        return response.data
    } 
    catch (error) {
        throw error
    }
}