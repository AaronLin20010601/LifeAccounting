import './assets/tailwind.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router/index'
import store from './store/index'
import axios from 'axios'
import VueApexCharts from "vue3-apexcharts";

const app = createApp(App)

store.dispatch('initState')

app.use(router)

app.use(store)

app.use(VueApexCharts);

app.config.globalProperties.$http = axios;

app.mount('#app')