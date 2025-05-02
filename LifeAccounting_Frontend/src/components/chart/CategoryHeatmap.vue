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
        </div>
    
        <!-- 圖表區域 -->
        <div ref="heatmap" class="border rounded"></div>
    </div>
</template>

<script>
import Plotly from 'plotly.js-dist-min'
import { fetchRecords } from '@/api/record'
import { fetchMeta } from '@/api/meta'
import { generateYearOptions } from '@/service/chartService';

export default {
    data() {
        return {
            accounts: [],
            selectedYear: new Date().getFullYear(),
            selectedAccountId: null,
            selectedType: 'Income',
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
            const start = new Date(this.selectedYear, this.selectedMonth ? this.selectedMonth - 1 : 0, 1)
            const end = new Date(this.selectedYear, this.selectedMonth ? this.selectedMonth : 12, 1)
            end.setDate(0)
            end.setHours(23, 59, 59, 999)

            const response = await fetchRecords({
                accountId: this.selectedAccountId,
                type: this.selectedType,
                startDate: start,
                endDate: end,
                page: 1,
                pageSize: 1000,
            })

            const dataMap = {}

            for (const record of response.items) {
                if (record.type !== this.selectedType) continue
                const month = new Date(record.date).getMonth()
                const category = record.categoryName || 'Other'
                const amount = Math.abs(record.amount)

                if (!dataMap[category]) {
                    dataMap[category] = Array(12).fill(0)
                }
                dataMap[category][month] += amount
            }

            const categories = Object.keys(dataMap)
            const zData = categories.map(c => dataMap[c])
            const months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']

            // 檢查是否有資料
            if (categories.length === 0) {
                Plotly.newPlot(this.$refs.heatmap, [], {
                    title: 'No data available',
                    xaxis: { visible: false },
                    yaxis: { visible: false },
                    annotations: [{
                        text: 'No data available for the selected filters',
                        showarrow: false,
                        font: { size: 18 },
                        xref: 'paper',
                        yref: 'paper',
                        x: 0.5,
                        y: 0.5,
                        xanchor: 'center',
                        yanchor: 'middle'
                    }],
                    margin: { t: 50, l: 50, r: 50, b: 50 },
                    height: 400,
                }, { responsive: true })
                return
            }

            const trace = {
                z: zData,
                x: months,
                y: categories,
                type: 'heatmap',
                colorscale: 'YlOrRd',
                hoverongaps: false,
            }

            const layout = {
                title: 'Monthly Heatmap by category',
                margin: { t: 50, l: 50, r: 50, b: 50 },
                height: 500,
                autosize: true,
                xaxis: {
                    automargin: true,
                },
                yaxis: {
                    automargin: true,
                },
            }

            Plotly.newPlot(this.$refs.heatmap, [trace], layout, { responsive: true })
        }
    }
}
</script>