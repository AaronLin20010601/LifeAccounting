<template>
    <!-- 分頁按鈕 -->
    <div v-if="totalPages > 1" class="flex justify-center mt-6 space-x-1 items-center">
        <!-- 第一頁與上一頁 -->
        <button @click="$emit('page-change', 1)" :disabled="currentPage === 1" class="px-2 py-1 border rounded-md">«</button>
        <button @click="$emit('page-change', currentPage - 1)" :disabled="currentPage === 1" class="px-2 py-1 border rounded-md">‹</button>
        
        <!-- 中間頁碼 -->
        <button
            v-for="page in visiblePages" :key="page" @click="$emit('page-change', page)"
            :class="[
                'px-3 py-1 border rounded-md', page === currentPage ? 
                'bg-blue-500 text-white' : 'bg-white text-black border-gray-300 hover:bg-gray-100'
            ]"
        >
            {{ page }}
        </button>
        
        <!-- 下一頁與最後一頁 -->
        <button @click="$emit('page-change', currentPage + 1)" :disabled="currentPage === totalPages" class="px-2 py-1 border rounded-md">›</button>
        <button @click="$emit('page-change', totalPages)" :disabled="currentPage === totalPages" class="px-2 py-1 border rounded-md">»</button>
    </div>
</template>
  
<script>
export default {
    props: {
        currentPage: Number,
        totalPages: Number,
    },
    computed: {
        // 可選換頁顯示範圍
        visiblePages() {
            const range = 2;
            const start = Math.max(1, this.currentPage - range);
            const end = Math.min(this.totalPages, this.currentPage + range);
            return Array.from({ length: end - start + 1 }, (_, i) => start + i);
        },
    },
    emits: ['page-change'],
};
</script>