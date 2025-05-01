<template>
    <div class="max-w-4xl mx-auto">
        <div class="flex gap-4 items-center mb-6">
            <!-- 年份選單 -->
            <label class="mr-2">Year:</label>
            <select v-model="selectedYear" @change="fetchChartData" class="p-2 border rounded">
                <option v-for="year in yearOptions" :key="year" :value="year">{{ year }}</option>
            </select>
    
            <!-- 月份選單 -->
            <label class="mr-2">Month:</label>
            <select v-model="selectedMonth" @change="fetchChartData" class="p-2 border rounded">
                <option :value="null">All</option>
                <option v-for="m in 12" :key="m" :value="m">{{ m }}</option>
            </select>
    
            <!-- 帳戶選單 -->
            <label class="mr-2">Account:</label>
            <select v-model="selectedAccountId" @change="fetchChartData" class="p-2 border rounded">
                <option :value="null">All</option>
                <option v-for="account in accounts" :key="account.id" :value="account.id">{{ account.name }}</option>
            </select>
        </div>
    
        <!-- 圖表區域 -->
        <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
            <div>
                <h2 class="text-xl font-semibold text-center mb-2">Income</h2>
                <div v-if="incomeSeries.length">
                    <apexchart type="pie" height="350" :options="incomeChartOptions" :series="incomeSeries" />
                </div>
                <div v-else class="h-[350px] flex items-center justify-center text-gray-500 border rounded">
                    No Data
                </div>
            </div>
            <div>
                <h2 class="text-xl font-semibold text-center mb-2">Expense</h2>
                <div v-if="expenseSeries.length">
                    <apexchart type="pie" height="350" :options="expenseChartOptions" :series="expenseSeries" />
                </div>
                <div v-else class="h-[350px] flex items-center justify-center text-gray-500 border rounded">
                    No Data
                </div>
            </div>
        </div>
    </div>
</template>
  
<script>
import ApexCharts from 'vue3-apexcharts';
import { fetchRecords } from '@/api/record';
import { fetchMeta } from '@/api/meta';
import { generateYearOptions, generateRandomColors } from '@/service/chartService';
  
export default {
    components: { apexchart: ApexCharts },
    data() {
        return {
            accounts: [],
            selectedYear: new Date().getFullYear(),
            selectedMonth: null,
            selectedAccountId: null,
            yearOptions: [],
            incomeSeries: [],
            expenseSeries: [],
            incomeChartOptions: { labels: [], colors: [], legend: { position: 'bottom' } },
            expenseChartOptions: { labels: [], colors: [], legend: { position: 'bottom' } },
        }
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
            const start = new Date(this.selectedYear, this.selectedMonth ? this.selectedMonth - 1 : 0, 1)
            const end = new Date(this.selectedYear, this.selectedMonth ? this.selectedMonth : 12, 1)
            end.setDate(0)
            end.setHours(23, 59, 59, 999)

            const response = await fetchRecords({
                accountId: this.selectedAccountId,
                startDate: start,
                endDate: end,
                page: 1,
                pageSize: 1000,
            })

            const incomeMap = {}
            const expenseMap = {}

            // 收支類別
            for (const record of response.items) {
                const categoryName = record.categoryName || 'Other'
                const amount = Math.abs(record.amount)

                if (record.type === 'Income') {
                    incomeMap[categoryName] = (incomeMap[categoryName] || 0) + amount
                } else if (record.type === 'Expense') {
                    expenseMap[categoryName] = (expenseMap[categoryName] || 0) + amount
                }
            }

            const incomeLabels = Object.keys(incomeMap)
            const expenseLabels = Object.keys(expenseMap)

            // 收入圖表
            this.incomeSeries = Object.values(incomeMap)
            this.incomeChartOptions = {
                labels: Object.keys(incomeMap),
                colors: generateRandomColors(incomeLabels.length, [180, 240]),
                legend: { position: 'bottom' },
            }

            // 支出圖表
            this.expenseSeries = Object.values(expenseMap)
            this.expenseChartOptions = {
                labels: Object.keys(expenseMap),
                colors: generateRandomColors(expenseLabels.length, [0, 50]),
                legend: { position: 'bottom' },
            }
        },
    },
}
</script>