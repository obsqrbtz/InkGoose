<template>
    <div class="relative flex flex-col items-center justify-center h-screen overflow-hidden">
        <div class="w-full p-6 bg-base-100  text-base-content border border-base-300 rounded-md border-top lg:max-w-lg">
            <h1 class="text-3xl font-semibold text-center text-content">
                ink goose
            </h1>
            <form class="space-y-4">
                <div>
                    <label class="label">
                        <span class="text-base label-text">Email</span>
                    </label>
                    <input
                        ref="email"
                        type="text"
                        placeholder="Email Address"
                        class="w-full input input-bordered"
                    >
                </div>
                <div>
                    <label class="label">
                        <span class="text-base label-text">Password</span>
                    </label>
                    <input
                        ref="password"
                        type="password"
                        placeholder="Enter Password"
                        class="w-full input input-bordered"
                    >
                </div>
                <div class="flex justify-center">
                    <button
                        class="btn btn-neutral mr-2"
                        @click.prevent="login()"
                    >
                        Login
                    </button>
                    <button
                        class="btn btn-neutral"
                        @click.prevent="toSignup()"
                    >
                        Signup
                    </button>
                </div>
            </form>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            data: null,
        };
    },
    created() {
        if (window.localStorage.getItem("accessToken")) {
            this.$router.push(this.$route.query.redirect || '/Notes')
        }
    },
    methods: {
        async login() {
            var email = this.$refs.email.value;
            var password = this.$refs.password.value;
            const response = await fetch(`${this.apiHost}/Users/Authenticate?email=${email}&password=${password}`, {
                method: "POST",
                headers: {
                    "Content-type": "application/json; charset=UTF-8"
                },
            });
            if (!response.ok) {
                alert("Invalid credentials. Please try again.");
                return;
            }
            var token = (await response.text()).toString()
            const checkAuth = await fetch(`${this.apiHost}/Users/SignIn`, {
                method: "GET",
                headers: {
                    "Content-type": "application/json; charset=UTF-8",
                    "Authorization": `Bearer ${token}`
                },
            });
            if (!checkAuth.ok) {
                alert("Failed to authenticate. Please try again.");
                return;
            }
            window.localStorage.setItem("email", email);
            window.localStorage.setItem("accessToken", token);
            this.$router.push(this.$route.query.redirect || '/Notes')
        },
        async toSignup() {
            this.$router.push(this.$route.query.redirect || '/Signup')
        },
    }
};
</script>