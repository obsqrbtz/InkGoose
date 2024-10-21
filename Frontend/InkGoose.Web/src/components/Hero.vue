<template>
    <div class="hero bg-base-100 -200 min-h-screen">
        <div class="hero-content flex-col lg:flex-row">
            <img
                src="../assets/logo.svg"
                class="max-w-sm rounded-lg"
            >
            <div>
                <h1 class="text-5xl font-bold">ink goose</h1>
                <p class="py-6">
                    Open source note taking app with markdown support.
                </p>
                <button
                    class="btn btn-neutral"
                    @click="toLogin"
                >
                    Get Started
                </button>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    methods: {
        async toLogin() {
            this.$router.push(this.$route.query.redirect || '/Login')
        },
    }
};
</script>