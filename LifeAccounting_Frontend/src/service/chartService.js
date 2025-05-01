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