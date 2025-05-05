import axios from 'axios'

const API_URL = 'http://localhost:5100/api/meta'
const getAuthHeaders = () => ({
    headers: {
        Authorization: `Bearer ${localStorage.getItem('token')}`,
    }
})

// 取得帳戶與分類元資料
export const fetchMeta = async ({ toCurrency = null } = {}) => {
    const params = new URLSearchParams()
    if (toCurrency) {
        params.append('toCurrency', toCurrency)
    }

    const response = await axios.get(`${API_URL}?${params.toString()}`, getAuthHeaders())
    return response.data
}