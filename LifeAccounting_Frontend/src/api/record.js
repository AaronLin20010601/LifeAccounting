import axios from 'axios'

const API_URL = 'http://localhost:5100/api/record'
const getAuthHeaders = () => ({
    headers: {
        Authorization: `Bearer ${localStorage.getItem('token')}`,
    }
})

// 取得收支紀錄列表
export const fetchRecords = async ({accountId = null, categoryId = null, type = null, 
    startDate = null, endDate = null, page, pageSize, toCurrency = null}) => {
    const params = new URLSearchParams({
        page,
        pageSize,
    });

    if (accountId) {
        params.append('accountId', accountId);
    }

    if (categoryId) {
        params.append('categoryId', categoryId);
    }

    if (type) {
        params.append('type', type)
    }

    if (startDate) {
        params.append('startDate', new Date(startDate).toISOString());
    }

    if (endDate) {
        params.append('endDate', new Date(endDate).toISOString());
    }

    if (toCurrency) {
        params.append('toCurrency', toCurrency)
    }

    const response = await axios.get(`${API_URL}?${params.toString()}`, getAuthHeaders());
    return response.data;
}

// 新增收支紀錄
export const createRecord = async (record) => {
    try {
        const response = await axios.post(API_URL, record, getAuthHeaders())
        return response.data
    } 
    catch (error) {
        throw error
    }
}

// 取得要編輯的紀錄內容
export const getEditRecord = async (id) => {
    try {
        const response = await axios.get(`${API_URL}/${id}`, getAuthHeaders())
        return response.data
    } 
    catch (error) {
        throw error
    }
}

// 更新收支紀錄
export const updateRecord = async (id, record) => {
    try {
        const response = await axios.patch(`${API_URL}/${id}`, record, getAuthHeaders())
        return response.data
    } 
    catch (error) {
        throw error
    }
}

// 刪除收支紀錄
export const deleteRecord = async (id) => {
    try {
        const response = await axios.delete(`${API_URL}/${id}`, getAuthHeaders())
        return response.data
    } 
    catch (error) {
        throw error
    }
}