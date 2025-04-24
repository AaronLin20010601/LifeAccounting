import axios from 'axios'

const API_URL = 'http://localhost:5100/api/record'
const getAuthHeaders = () => ({
    headers: {
        Authorization: `Bearer ${localStorage.getItem('token')}`,
    }
})

// 取得帳戶與分類元資料
export const fetchRecordMeta = async () => {
    const response = await axios.get(`${API_URL}/meta`, getAuthHeaders())
    return response.data
}

// 取得收支紀錄列表
export const fetchRecords = async ({accountId = null, categoryId = null, type, startDate = null, endDate = null, page, pageSize}) => {
    const params = new URLSearchParams({
        type,
        page,
        pageSize,
    });

    if (accountId){
        params.append('accountId', accountId);
    }

    if (categoryId){
        params.append('categoryId', categoryId);
    }

    if (startDate) {
        params.append('startDate', new Date(startDate).toISOString());
    }

    if (endDate) {
        params.append('endDate', new Date(endDate).toISOString());
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