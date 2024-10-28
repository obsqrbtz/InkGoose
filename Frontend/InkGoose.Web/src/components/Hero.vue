<template>
    <div class="hero bg-base-100 -200 min-h-screen">
        <div class="hero-content flex-col lg:flex-row">
            <img
                src="../assets/logo.svg"
                class="max-w-sm rounded-lg"
            >
            <div>
                <h1 class="text-5xl font-bold ml-1">ink goose</h1>
                <p class="py-6 ml-1">
                    Open source note taking app with markdown support.
                </p>
                <nav class="flex-col pb-4 ml-1">
                    <a
                        class="inline-block items-center justify-center align-middle"
                        href="https://github.com/obsqrbtz/inkgoose"
                    >
                        <svg
                            aria-hidden="true"
                            class="octicon octicon-mark-github fill-current inline-block mr-2"
                            height="24"
                            version="1.1"
                            viewBox="0 0 16 16"
                            width="24"
                        ><path
                            fill-rule="evenodd"
                            d="M8 0C3.58 0 0 3.58 0 8c0 3.54 2.29 6.53 5.47 7.59.4.07.55-.17.55-.38 0-.19-.01-.82-.01-1.49-2.01.37-2.53-.49-2.69-.94-.09-.23-.48-.94-.82-1.13-.28-.15-.68-.52-.01-.53.63-.01 1.08.58 1.23.82.72 1.21 1.87.87 2.33.66.07-.52.28-.87.51-1.07-1.78-.2-3.64-.89-3.64-3.95 0-.87.31-1.59.82-2.15-.08-.2-.36-1.02.08-2.12 0 0 .67-.21 2.2.82.64-.18 1.32-.27 2-.27.68 0 1.36.09 2 .27 1.53-1.04 2.2-.82 2.2-.82.44 1.1.16 1.92.08 2.12.51.56.82 1.27.82 2.15 0 3.07-1.87 3.75-3.65 3.95.29.25.54.73.54 1.48 0 1.07-.01 1.93-.01 2.2 0 .21.15.46.55.38A8.013 8.013 0 0 0 16 8c0-4.42-3.58-8-8-8z"
                        />
                        </svg>
                        <p class="inline-block align-middle ml-2">
                            View on GitHub
                        </p>
                    </a>
                </nav>
                <button
                    class="btn mt-1"
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