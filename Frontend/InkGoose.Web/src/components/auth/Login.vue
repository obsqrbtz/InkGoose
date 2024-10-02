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
                    <input ref="password" type="password" placeholder="Enter Password" class="w-full input input-bordered" />
                </div>
                <a href="#" class="text-xs text-gray-600 hover:underline hover:text-blue-600">Forgot Password?</a>
                <div>
                    <button @click.prevent="login()" class="btn btn-block btn-neutral">Login</button>
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
            if (response.ok) {
                var token = (await response.text()).toString()
                window.localStorage.setItem("accessToken", token);
                const checkAuth = await fetch(`${this.apiHost}/Users/SignIn`, {
                method: "GET",
                headers: {
                    "Content-type": "application/json; charset=UTF-8",
                    "Authorization": `Bearer ${window.localStorage.getItem("accessToken")}`
                },
                });
            alert((await checkAuth.text()).toString())
            }
        },
    }
};
</script>