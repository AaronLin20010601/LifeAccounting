<template>
    <div class="max-w-5xl mx-auto px-4 py-6">
        <h1 class="text-2xl font-bold mb-4">Financial Charts</h1>
    
        <!-- 圖表選單 -->
        <div class="mb-6">
            <label class="mr-2 font-medium">Select Chart:</label>
            <select v-model="selectedChart" class="p-2 border rounded">
                <option v-for="chart in chartOptions" :key="chart.value" :value="chart.value">
                    {{ chart.label }}
                </option>
            </select>
        </div>
        
        <!-- 圖表顯示區 -->
        <div class="border rounded p-4 shadow">
            <component :is="currentChartComponent" />
        </div>
    </div>
</template>
  
<script>
import MonthlyLineChart from '@/components/chart/MonthlyLineChart.vue'
import IncomeExpensePieChart from '@/components/chart/IncomeExpensePieChart.vue';
import AccountDonutChart from '@/components/chart/AccountDonutChart.vue';
import CategoryHeatmap from '@/components/chart/CategoryHeatmap.vue';

export default {
    components: {
        MonthlyLineChart,
        IncomeExpensePieChart,
        AccountDonutChart,
        CategoryHeatmap
    },
    data() {
        return {
            selectedChart: 'monthly-line',
            chartOptions: [
                { value: 'monthly-line', label: 'Monthly Income & Expense' },
                { value: 'income-expense-pie', label: 'Income & Expense by category'},
                { value: 'account-donut', label: 'Account Balance, Income & Expense by account'},
                { value: 'category-heat', label: 'Heatmap by category'}
            ],
        };
    },
    computed: {
        // 選擇顯示圖表
        currentChartComponent() {
            switch (this.selectedChart) {
                case 'monthly-line':
                    return 'MonthlyLineChart';
                case 'income-expense-pie':
                    return 'IncomeExpensePieChart';
                case 'account-donut':
                    return 'AccountDonutChart';
                case 'category-heat':
                    return 'CategoryHeatmap'
                default:
                    return null;
            }
        },
    },
};
</script>