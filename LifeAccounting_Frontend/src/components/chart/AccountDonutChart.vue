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
        </div>
    
        <!-- 圖表區域 -->
        <div class="grid grid-cols-1 md:grid-cols-3 gap-6">
            <div>
                <h2 class="text-center font-semibold mb-2">Current Account Balance</h2>
                <div v-if="balanceSeries.length">
                    <apexchart type="donut" height="300" :options="balanceChartOptions" :series="balanceSeries" />
                </div>
                <div v-else class="h-[300px] flex items-center justify-center text-gray-500 border rounded">No Data</div>
            </div>

            <div>
                <h2 class="text-center font-semibold mb-2">Income</h2>
                <div v-if="incomeSeries.length">
                    <apexchart type="donut" height="300" :options="incomeChartOptions" :series="incomeSeries" />
                </div>
                <div v-else class="h-[300px] flex items-center justify-center text-gray-500 border rounded">No Data</div>
            </div>

            <div>
                <h2 class="text-center font-semibold mb-2">Expense</h2>
                <div v-if="expenseSeries.length">
                    <apexchart type="donut" height="300" :options="expenseChartOptions" :series="expenseSeries" />
                </div>
                <div v-else class="h-[300px] flex items-center justify-center text-gray-500 border rounded">No Data</div>
            </div>
        </div>
    </div>
</template>
  
<script>
import ApexCharts from 'vue3-apexcharts'
import { fetchRecords } from '@/api/record'
import { fetchMeta } from '@/api/meta'
import { generateYearOptions, generateRandomColors, generateGrayScaleColors } from '@/service/chartService';
  
export default {
    components: { apexchart: ApexCharts },
    data() {
        return {
            selectedYear: new Date().getFullYear(),
            selectedMonth: null,
            yearOptions: [],
            accounts: [],
            balanceSeries: [],
            incomeSeries: [],
            expenseSeries: [],
            balanceChartOptions: { labels: [], colors: [], legend: { position: 'bottom' } },
            incomeChartOptions: { labels: [], colors: [], legend: { position: 'bottom' } },
            expenseChartOptions: { labels: [], colors: [], legend: { position: 'bottom' } },
        }
    },
    async mounted() {
        this.yearOptions = generateYearOptions()
        await this.fetchMetaData()
        await this.fetchChartData()
    },
    methods: {
        async fetchMetaData() {
            const meta = await fetchMeta()
            this.accounts = meta.accounts
        },
        // 圖表資料
        async fetchChartData() {
            const start = new Date(this.selectedYear, this.selectedMonth ? this.selectedMonth - 1 : 0, 1)
            const end = new Date(this.selectedYear, this.selectedMonth ? this.selectedMonth : 12, 1)
            end.setDate(0)
            end.setHours(23, 59, 59, 999)

            const [meta, response] = await Promise.all([
                fetchMeta(),
                fetchRecords({
                    startDate: start,
                    endDate: end,
                    page: 1,
                    pageSize: 1000,
                }),
            ])

            this.accounts = meta.accounts

            // 帳戶餘額分布
            const balances = this.accounts.map(a => a.balance || 0)
            const accountNames = this.accounts.map(a => a.name)
            this.balanceSeries = balances
            this.balanceChartOptions = {
                labels: accountNames,
                colors: generateGrayScaleColors(accountNames.length),
                legend: { position: 'bottom' },
            }

            // 收支分布
            const incomeMap = {}
            const expenseMap = {}

            for (const record of response.items) {
                const name = this.accounts.find(a => a.id === record.accountId)?.name || 'Unknown'
                const amount = Math.abs(record.amount)

                if (record.type === 'Income') {
                    incomeMap[name] = (incomeMap[name] || 0) + amount
                } else if (record.type === 'Expense') {
                    expenseMap[name] = (expenseMap[name] || 0) + amount
                }
            }

            // 帳戶收入
            const incomeLabels = Object.keys(incomeMap)
            this.incomeSeries = Object.values(incomeMap)
            this.incomeChartOptions = {
                labels: incomeLabels,
                colors: generateRandomColors(incomeLabels.length, [180, 240]),
                legend: { position: 'bottom' },
            }

            // 帳戶支出
            const expenseLabels = Object.keys(expenseMap)
            this.expenseSeries = Object.values(expenseMap)
            this.expenseChartOptions = {
                labels: expenseLabels,
                colors: generateRandomColors(expenseLabels.length, [0, 50]),
                legend: { position: 'bottom' },
            }
        },
    },
}
</script>