<template>
    <div class="relative flex flex-col items-center justify-center h-screen overflow-hidden">
        <div class="w-full p-6 bg-white border-t-4 border-gray-600 rounded-md shadow-md border-top lg:max-w-lg">
            <h1 class="text-3xl font-semibold text-center text-gray-700">
                ink goose
            </h1>
            <form class="space-y-4">
                <div>
                    <label class="label">
                        <span class="text-base label-text">Email</span>
                    </label>
                    <input ref="email" type="text" placeholder="Email Address" class="w-full input input-bordered" />
                </div>
                <div>
                    <label class="label">
                        <span class="text-base label-text">Password</span>
                    </label>
                    <input ref="password" type="password" placeholder="Enter Password"
                        class="w-full input input-bordered" />
                </div>
                <div class="flex justify-center">
                    <button @click.prevent="login()" class="btn btn-neutral mr-2">Login</button>
                    <button @click.prevent="toSignup()" class="btn btn-neutral">Signup</button>
                </div>
            </form>
        </div>
    </div>
</template>

<script>
export default {
    created() {
        if (window.localStorage.getItem("accessToken")) {
            this.$router.push(this.$route.query.redirect || '/Notes')
        }
    },
    data() {
        return {
            data: null,
        };
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
                    "Authorization": `Bearer ${window.localStorage.getItem("accessToken")}`
                },
            });
            if (!response.ok) {
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