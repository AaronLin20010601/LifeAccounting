import { createStore } from 'vuex'

const store = createStore({
    state: {
        token: null,
        user: null,
        isLoggedIn: false,
    },
    mutations: {
        // 設定 token 和 user
        setToken(state, token) {
            state.token = token;
        },
        setUser(state, user) {
            state.user = user;
        },
        // 設定登入狀態
        setLoginState(state, isLoggedIn) {
            state.isLoggedIn = isLoggedIn;
        },
        // 清除登入狀態
        clearLoginState(state) {
            state.token = null;
            state.user = null;
            state.isLoggedIn = false;
        }
    },
    actions: {
        // 用於登錄操作
        login({ commit }, { token, user }) {
            commit('setToken', token);
            commit('setUser', user);
            commit('setLoginState', true);

            // 儲存至 localStorage
            localStorage.setItem('token', token);
            localStorage.setItem('user', JSON.stringify(user));
        },

        // 用於登出操作
        logout({ commit }) {
            commit('clearLoginState');

            // 清除 localStorage
            localStorage.removeItem('token');
            localStorage.removeItem('user');
        },

        // 從 localStorage 初始化狀態
        initState({ commit }) {
            const token = localStorage.getItem('token');
            const user = JSON.parse(localStorage.getItem('user'));
            if (token && user) {
                commit('setToken', token);
                commit('setUser', user);
                commit('setLoginState', true);
            }
        }
    },
    getters: {
        isLoggedIn(state) {
            return state.isLoggedIn;
        },
        getUser(state) {
            return state.user;
        },
        getToken(state) {
            return state.token;
        }
    }
})

export default store;