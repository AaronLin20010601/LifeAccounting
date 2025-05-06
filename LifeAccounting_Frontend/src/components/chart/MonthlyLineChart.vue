<template>
    <div class="max-w-4xl mx-auto">
        <div class="flex gap-4 items-center mb-6">
            <!-- 年份選單 -->
            <label class="mr-2">Year:</label>
            <select v-model="selectedYear" @change="fetchChartData" class="p-2 border rounded">
                <option v-for="year in yearOptions" :key="year" :value="year">{{ year }}</option>
            </select>
    
            <!-- 帳戶選單 -->
            <label class="mr-2">Account:</label>
            <select v-model="selectedAccountId" @change="fetchChartData" class="p-2 border rounded">
                <option :value="null">All</option>
                <option v-for="account in accounts" :key="account.id" :value="account.id">{{ account.name }}</option>
            </select>

            <!-- 幣別選單 -->
            <label class="mr-2">Currency:</label>
            <select v-model="selectedCurrency" @change="fetchChartData" class="p-2 border rounded">
                <option value="TWD">TWD</option>
                <option value="USD">USD</option>
                <option value="JPY">JPY</option>
            </select>
        </div>
    
        <!-- 圖表區域 -->
        <apexchart type="line" height="400" width="600" :options="chartOptions" :series="chartSeries"/>
    </div>
</template>
  
<script>
import ApexCharts from 'vue3-apexcharts';
import { fetchMeta } from '@/api/meta';
import { generateYearOptions, getMonthlyIncomeExpenseData } from '@/service/chartService';
  
export default {
    components: { apexchart: ApexCharts },
    data() {
        return {
            accounts: [],
            selectedAccountId: null,
            selectedYear: new Date().getFullYear(),
            selectedCurrency: 'TWD',
            yearOptions: [],
            chartSeries: [],
            chartOptions: {
                chart: {
                    type: 'line',
                    toolbar: { show: false },
                },
                stroke: {
                    curve: 'smooth',
                    width: 2,
                },
                colors: ['#00b894', '#d63031'],
                // 橫軸月份
                xaxis: {
                    categories: [
                        'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
                        'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'
                    ],
                },
                // 縱軸金額
                yaxis: {
                    title: { text: 'Amount' },
                    labels: {
                        formatter: (val) => `$${val}`,
                    },
                },
                tooltip: {
                    y: {
                        formatter: (val) => `$${parseFloat(val.toFixed(2)).toString()}`,
                    },
                },
                legend: {
                    position: 'top',
                },
            },
        };
    },
    async mounted() {
        this.yearOptions = generateYearOptions();
        await this.fetchMetaData();
        await this.fetchChartData();
    },
    methods: {
        async fetchMetaData() {
            const meta = await fetchMeta();
            this.accounts = meta.accounts;
        },
        // 圖表資料
        async fetchChartData() {
            this.chartSeries = await getMonthlyIncomeExpenseData({
                year: this.selectedYear,
                accountId: this.selectedAccountId,
                toCurrency: this.selectedCurrency,
            });
        },
    },
};
</script>