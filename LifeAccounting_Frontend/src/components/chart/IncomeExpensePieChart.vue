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

            <!-- 幣別選單 -->
            <label class="mr-2">Currency:</label>
            <select v-model="selectedCurrency" @change="fetchChartData" class="p-2 border rounded">
                <option value="TWD">TWD</option>
                <option value="USD">USD</option>
                <option value="JPY">JPY</option>
            </select>
        </div>
    
        <!-- 圖表區域 -->
        <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
            <PieChart title="Income" :series="incomeSeries" :labels="incomeLabels" :colors="incomeColors"/>
            <PieChart title="Expense" :series="expenseSeries" :labels="expenseLabels" :colors="expenseColors"/>
        </div>
    </div>
</template>
  
<script>
import PieChart from '@/components/chart/PieChart.vue';
import { fetchMeta } from '@/api/meta';
import { generateYearOptions, getIncomeExpensePieData } from '@/service/chartService';
  
export default {
    components: { PieChart },
    data() {
        return {
            accounts: [],
            selectedYear: new Date().getFullYear(),
            selectedMonth: null,
            selectedAccountId: null,
            selectedCurrency: 'TWD',
            yearOptions: [],
            incomeLabels: [],
            incomeSeries: [],
            incomeColors: [],
            expenseLabels: [],
            expenseSeries: [],
            expenseColors: [],
        }
    },
    async mounted() {
        this.yearOptions = generateYearOptions();
        await this.fetchMetaData();
        await this.fetchChartData();
    },
    methods: {
        async fetchMetaData() {
            const meta = await fetchMeta({ toCurrency: this.selectedCurrency });
            this.accounts = meta.accounts;
        },
        // 圖表資料
        async fetchChartData() {
            const chartData = await getIncomeExpensePieData({
                year: this.selectedYear,
                month: this.selectedMonth,
                accountId: this.selectedAccountId,
                currency: this.selectedCurrency,
            });

            this.incomeLabels = chartData.income.labels;
            this.incomeSeries = chartData.income.series;
            this.incomeColors = chartData.income.colors;

            this.expenseLabels = chartData.expense.labels;
            this.expenseSeries = chartData.expense.series;
            this.expenseColors = chartData.expense.colors;
        },
    },
}
</script>