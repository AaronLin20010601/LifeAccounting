import Plotly from 'plotly.js-dist-min'
import { fetchRecordsForChart } from '@/api/record';
import { fetchMeta } from '@/api/meta'

// 可選年份
export function generateYearOptions() {
    const currentYear = new Date().getFullYear();
    const years = []
    for (let y = 2000; y <= currentYear; y++) {
        years.unshift(y);
    }
    return years
}

// 隨機生成顏色
export function generateRandomColors(count, hueRange) {
    const colors = []
    for (let i = 0; i < count; i++) {
        const hue = Math.floor(Math.random() * (hueRange[1] - hueRange[0] + 1)) + hueRange[0]
        const saturation = Math.floor(Math.random() * 20) + 70
        const lightness = Math.floor(Math.random() * 20) + 50
        colors.push(`hsl(${hue}, ${saturation}%, ${lightness}%)`)
    }
    return colors
}

// 隨機生成黑灰色系顏色
export function generateGrayScaleColors(count) {
    const colors = []
    for (let i = 0; i < count; i++) {
        const lightness = Math.floor(Math.random() * 50) + 30
        colors.push(`hsl(0, 0%, ${lightness}%)`)
    }
    return colors
}

// 生成收支折線圖
export async function getMonthlyIncomeExpenseData({ year, accountId, toCurrency }) {
    const startDate = new Date(`${year}-01-01`);
    const endDate = new Date(`${year}-12-31`);

    const response = await fetchRecordsForChart({
        accountId,
        startDate,
        endDate,
        toCurrency,
    });

    // 月收支折線圖
    const { incomeMap, expenseMap } = response.items.reduce((accumulator, record) => {
        const month = new Date(record.date).getMonth();
        const amount = Math.abs(record.amount);

        if (record.type === 'Income') {
            accumulator.incomeMap[month] += amount;
        } else if (record.type === 'Expense') {
            accumulator.expenseMap[month] += amount;
        }

        return accumulator;
    }, {
        incomeMap: Array(12).fill(0),
        expenseMap: Array(12).fill(0)
    });

    return [
        { name: 'Income', data: incomeMap },
        { name: 'Expense', data: expenseMap },
    ];
}

// 生成收支類別圓餅圖
export async function getIncomeExpensePieData({ year, month, accountId, currency }) {
    const start = new Date(year, month ? month - 1 : 0, 1);
    const end = new Date(year, month ? month : 12, 1);
    end.setDate(0);
    end.setHours(23, 59, 59, 999);

    const response = await fetchRecordsForChart({
        accountId,
        startDate: start,
        endDate: end,
        toCurrency: currency,
    });

    // 收支類別
    const { incomeMap, expenseMap } = response.items.reduce((accumulator, record) => {
        const categoryName = record.categoryName || 'Other';
        const amount = Math.abs(record.amount);

        if (record.type === 'Income') {
            accumulator.incomeMap[categoryName] = (accumulator.incomeMap[categoryName] || 0) + amount;
        } else if (record.type === 'Expense') {
            accumulator.expenseMap[categoryName] = (accumulator.expenseMap[categoryName] || 0) + amount;
        }

        return accumulator;
    }, {
        incomeMap: {},
        expenseMap: {}
    });

    // 收入類別
    const incomeLabels = Object.keys(incomeMap);
    const incomeSeries = Object.values(incomeMap);
    const incomeColors = generateRandomColors(incomeLabels.length, [180, 240]);

    // 支出類別
    const expenseLabels = Object.keys(expenseMap);
    const expenseSeries = Object.values(expenseMap);
    const expenseColors = generateRandomColors(expenseLabels.length, [0, 50]);

    return {
        income: { labels: incomeLabels, series: incomeSeries, colors: incomeColors },
        expense: { labels: expenseLabels, series: expenseSeries, colors: expenseColors },
    };
}

// 生成帳戶餘額和收支分布圓環圖
export async function getAccountDonutData({ year, month, currency }) {
    const start = new Date(year, month ? month - 1 : 0, 1)
    const end = new Date(year, month ? month : 12, 1)
    end.setDate(0)
    end.setHours(23, 59, 59, 999)
    
    // 取得帳戶和收支紀錄
    const [meta, response] = await Promise.all([
        fetchMeta({ toCurrency: currency }),
        fetchRecordsForChart({
            startDate: start,
            endDate: end,
            toCurrency: currency,
        }),
    ])
  
    const accounts = meta.accounts
  
    // 帳戶餘額分布
    const balanceLabels = accounts.map(a => a.name);
    const balanceSeries = accounts.map(a => a.balance || 0);
    const balanceColors = generateGrayScaleColors(balanceLabels.length);
  
    // 收支分布
    const accountIdToName = Object.fromEntries(accounts.map(a => [a.id, a.name]));
    const { incomeMap, expenseMap } = response.items.reduce((accumulator, record) => {
        const name = accountIdToName[record.accountId] || 'Unknown';
        const amount = Math.abs(record.amount);

        if (record.type === 'Income') {
            accumulator.incomeMap[name] = (accumulator.incomeMap[name] || 0) + amount;
        } else if (record.type === 'Expense') {
            accumulator.expenseMap[name] = (accumulator.expenseMap[name] || 0) + amount;
        }

        return accumulator;
    }, {
        incomeMap: {},
        expenseMap: {}
    });
    
    // 帳戶收入
    const incomeLabels = Object.keys(incomeMap);
    const incomeSeries = Object.values(incomeMap);
    const incomeColors = generateRandomColors(incomeLabels.length, [180, 240]);
    
    // 帳戶支出
    const expenseLabels = Object.keys(expenseMap);
    const expenseSeries = Object.values(expenseMap);
    const expenseColors = generateRandomColors(expenseLabels.length, [0, 50]);
  
    return {
        balance: { labels: balanceLabels, series: balanceSeries, colors: balanceColors },
        income: { labels: incomeLabels, series: incomeSeries, colors: incomeColors },
        expense: { labels: expenseLabels, series: expenseSeries, colors: expenseColors },
    }
}

// 生成熱力圖
export async function drawCategoryHeatmap({ containerRef, selectedYear, selectedAccountId, selectedType, selectedCurrency }) {
    const start = new Date(selectedYear, 0, 1)
    const end = new Date(selectedYear, 11, 31, 23, 59, 59, 999)

    const response = await fetchRecordsForChart({
        accountId: selectedAccountId,
        type: selectedType,
        startDate: start,
        endDate: end,
        toCurrency: selectedCurrency,
    })

    // 收支紀錄
    const dataMap = response.items.reduce((map, record) => {
        if (record.type !== selectedType) return map;

        const month = new Date(record.date).getMonth();
        const category = record.categoryName || 'Other';
        const amount = Math.abs(record.amount);

        if (!map[category]) {
            map[category] = Array(12).fill(0);
        }
        map[category][month] += amount;

        return map;
    }, {});

    const categories = Object.keys(dataMap)
    const zData = categories.map(c => dataMap[c])
    const months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']

    // 檢查是否有資料
    if (categories.length === 0) {
        Plotly.newPlot(containerRef, [], {
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
        xaxis: { automargin: true },
        yaxis: { automargin: true },
    }

    Plotly.newPlot(containerRef, [trace], layout, { responsive: true })
}