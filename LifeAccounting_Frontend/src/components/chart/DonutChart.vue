<template>
    <div>
        <h2 class="text-center font-semibold mb-2">{{ title }}</h2>
        <div v-if="series.length">
            <apexchart type="donut" height="300" :options="chartOptions" :series="series" />
        </div>
        <div v-else class="h-[300px] flex items-center justify-center text-gray-500 border rounded">No Data</div>
    </div>
</template>
  
<script>
import ApexCharts from 'vue3-apexcharts'
  
export default {
    components: { apexchart: ApexCharts },
    props: {
        title: String,
        series: Array,
        labels: Array,
        colors: Array,
    },
    computed: {
        chartOptions() {
            return {
                labels: this.labels,
                colors: this.colors,
                legend: { position: 'bottom' },
                tooltip: {
                    y: {
                        formatter: (val) => parseFloat(val.toFixed(2)).toString(),
                    },
                },
            }
        },
    },
}
</script>