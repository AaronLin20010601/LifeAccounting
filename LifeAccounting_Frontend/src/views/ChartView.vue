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

export default {
    components: {
        MonthlyLineChart,
    },
    data() {
        return {
            selectedChart: 'monthly-line',
            chartOptions: [
                { value: 'monthly-line', label: 'Monthly Income & Expense' },
            ],
        };
    },
    computed: {
        currentChartComponent() {
            switch (this.selectedChart) {
                case 'monthly-line':
                    return 'MonthlyLineChart';
                // case 'category-pie':
                //   return 'CategoryPieChart';
                default:
                    return null;
            }
        },
    },
};
</script>