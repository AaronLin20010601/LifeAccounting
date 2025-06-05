<template>
    <div class="w-full max-w-4xl mx-auto text-center space-y-8">
        <div>
            <h1 class="text-4xl md:text-5xl font-bold text-gray-800">Take Control of Your Finances</h1>
            <p class="mt-4 text-lg text-gray-600">Life Accounting helps you effortlessly track your expenses, analyze your spending, and make smarter financial decisions.</p>
        </div>

        <div class="flex justify-center">
            <img src="@/assets/metrics.svg" class="w-64 md:w-80 h-auto object-contain" />
        </div>

        <div class="flex justify-center gap-4">
            <RouterLink to="/login">
                <button class="bg-green-500 text-white px-6 py-3 rounded-xl shadow hover:bg-green-600 transition">Login</button>
            </RouterLink>
            <RouterLink to="/register">
                <button class="bg-gray-200 text-gray-800 px-6 py-3 rounded-xl shadow hover:bg-gray-300 transition">Register</button>
            </RouterLink>
        </div>
    </div>
</template>