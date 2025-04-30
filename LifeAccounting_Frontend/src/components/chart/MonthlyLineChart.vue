<template>
    <div class="max-w-4xl mx-auto">
        <div class="flex gap-4 items-center mb-6">
            <!-- 年份選單 -->
            <div>
                <label class="mr-2">Year:</label>
                <select v-model="selectedYear" @change="fetchChartData" class="p-2 border rounded">
                    <option v-for="year in yearOptions" :key="year" :value="year">{{ year }}</option>
                </select>
            </div>
    
            <!-- 帳戶選單 -->
            <div>
                <label class="mr-2">Account:</label>
                <select v-model="selectedAccountId" @change="fetchChartData" class="p-2 border rounded">
                    <option :value="null">All</option>
                    <option v-for="account in accounts" :key="account.id" :value="account.id">{{ account.name }}</option>
                </select>
            </div>
        </div>
    
        <!-- 圖表區域 -->
        <apexchart type="line" height="400" :options="chartOptions" :series="chartSeries"/>
    </div>
</template>
  
<script>
import { fetchRecordMeta, fetchRecords } from '@/api/record';
import ApexCharts from 'vue3-apexcharts';
  
export default {
    name: 'MonthlyLineChart',
    components: { apexchart: ApexCharts },
    data() {
        return {
            accounts: [],
            selectedAccountId: null,
            selectedYear: new Date().getFullYear(),
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
                        formatter: (val) => `$${val}`,
                    },
                },
                legend: {
                    position: 'top',
                },
            },
        };
    },
    async mounted() {
        this.generateYearOptions();
        await this.fetchMeta();
        await this.fetchChartData();
    },
    methods: {
        // 可選年份
        generateYearOptions() {
            const currentYear = new Date().getFullYear();
            for (let y = 2000; y <= currentYear; y++) {
            this.yearOptions.unshift(y);
            }
        },
        async fetchMeta() {
            const meta = await fetchRecordMeta();
            this.accounts = meta.accounts;
        },
        // 圖表資料
        async fetchChartData() {
            const startDate = new Date(`${this.selectedYear}-01-01`);
            const endDate = new Date(`${this.selectedYear}-12-31`);
    
            const incomeMap = Array(12).fill(0);
            const expenseMap = Array(12).fill(0);
    
            const response = await fetchRecords({
                accountId: this.selectedAccountId,
                startDate,
                endDate,
                page: 1,
                pageSize: 1000,
            });
    
            for (const record of response.items) {
                const date = new Date(record.date);
                const monthIndex = date.getMonth();
                const amount = Math.abs(record.amount);
        
                if (record.type === 'Income') {
                    incomeMap[monthIndex] += amount;
                }
                else if (record.type === 'Expense') {
                    expenseMap[monthIndex] += amount;
                }
            }
    
            this.chartSeries = [
                {
                    name: 'Income',
                    data: incomeMap,
                },
                {
                    name: 'Expense',
                    data: expenseMap,
                },
            ];
        },
    },
};
</script>