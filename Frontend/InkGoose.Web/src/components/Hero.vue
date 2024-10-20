<template>
    <div class="hero bg-base-100 -200 min-h-screen">
        <div class="hero-content flex-col lg:flex-row">
            <img src="https://i.pinimg.com/originals/85/c4/90/85c49013ac7c2d619f57b9a5b8b05b63.jpg"
                class="max-w-sm rounded-lg shadow-2xl" />
            <div>
                <h1 class="text-5xl font-bold">ink goose</h1>
                <p class="py-6">
                    Open source note taking app with markdown support.
                </p>
                <button @click="toLogin" class="btn btn-neutral">Get Started</button>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
export default {
    methods: {
        async toLogin() {
            this.$router.push(this.$route.query.redirect || '/Login')
        },
    }
};
</script>