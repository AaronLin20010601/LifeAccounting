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

            <!-- 幣別選單 -->
            <label class="mr-2">Currency:</label>
            <select v-model="selectedCurrency" @change="fetchChartData" class="p-2 border rounded">
                <option value="TWD">TWD</option>
                <option value="USD">USD</option>
                <option value="JPY">JPY</option>
            </select>
        </div>
    
        <!-- 圖表區域 -->
        <div class="grid grid-cols-1 md:grid-cols-3 gap-6">
            <DonutChart title="Current Account Balance" :series="balanceSeries" :labels="balanceLabels" :colors="balanceColors"/>
            <DonutChart title="Income" :series="incomeSeries" :labels="incomeLabels" :colors="incomeColors"/>
            <DonutChart title="Expense" :series="expenseSeries" :labels="expenseLabels" :colors="expenseColors"/>
        </div>
    </div>
</template>
  
<script>
import DonutChart from '@/components/chart/DonutChart.vue';
import { fetchMeta } from '@/api/meta';
import { generateYearOptions, getAccountDonutData } from '@/service/chartService';
  
export default {
    components: { DonutChart },
    data() {
        return {
            selectedYear: new Date().getFullYear(),
            selectedMonth: null,
            selectedCurrency: 'TWD',
            yearOptions: [],
            balanceLabels: [],
            balanceSeries: [],
            balanceColors: [],
            incomeLabels: [],
            incomeSeries: [],
            incomeColors: [],
            expenseLabels: [],
            expenseSeries: [],
            expenseColors: [],
        }
    },
    async mounted() {
        this.yearOptions = generateYearOptions()
        await this.fetchMetaData()
        await this.fetchChartData()
    },
    methods: {
        async fetchMetaData() {
            const meta = await fetchMeta({ toCurrency: this.selectedCurrency })
            this.accounts = meta.accounts
        },
        // 圖表資料
        async fetchChartData() {
            const chartData = await getAccountDonutData({
                year: this.selectedYear,
                month: this.selectedMonth,
                currency: this.selectedCurrency,
            })

            this.balanceLabels = chartData.balance.labels
            this.balanceSeries = chartData.balance.series
            this.balanceColors = chartData.balance.colors

            this.incomeLabels = chartData.income.labels
            this.incomeSeries = chartData.income.series
            this.incomeColors = chartData.income.colors

            this.expenseLabels = chartData.expense.labels
            this.expenseSeries = chartData.expense.series
            this.expenseColors = chartData.expense.colors
        },
    },
}
</script>