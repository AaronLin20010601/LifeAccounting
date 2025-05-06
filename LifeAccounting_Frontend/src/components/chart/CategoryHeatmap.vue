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

            <!-- 收支類型 -->
            <label class="mr-2">Type:</label>
            <select v-model="selectedType" @change="fetchChartData" class="p-2 border rounded-md">
                <option value="Income">Income</option>
                <option value="Expense">Expense</option>
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
        <div ref="heatmap" class="border rounded"></div>
    </div>
</template>

<script>
import { fetchMeta } from '@/api/meta'
import { generateYearOptions, drawCategoryHeatmap } from '@/service/chartService';

export default {
    data() {
        return {
            accounts: [],
            selectedYear: new Date().getFullYear(),
            selectedAccountId: null,
            selectedType: 'Income',
            selectedCurrency: 'TWD',
            yearOptions: [],
        }
    },
    async mounted() {
        this.yearOptions = generateYearOptions();
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
            await drawCategoryHeatmap({
                containerRef: this.$refs.heatmap,
                selectedYear: this.selectedYear,
                selectedAccountId: this.selectedAccountId,
                selectedType: this.selectedType,
                selectedCurrency: this.selectedCurrency,
            })
        }
    }
}
</script>