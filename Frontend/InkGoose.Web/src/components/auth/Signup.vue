<template>
    <div class="relative flex flex-col items-center justify-center h-screen overflow-hidden">
        <div class="w-full p-6 bg-base-100  text-base-content border border-base-300 rounded-md3 lg:max-w-lg">
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
                        <span class="text-base label-text">User name</span>
                    </label>
                    <input
                        ref="username"
                        type="text"
                        placeholder="User name"
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
                <div>
                    <button
                        class="btn btn-block btn-neutral"
                        @click.prevent="signin()"
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
        async signin() {
            var email = this.$refs.email.value;
            var username = this.$refs.username.value;
            var password = this.$refs.password.value;
            const response = await fetch(`${this.apiHost}/Users/AddUser?userName=${username}&email=${email}&password=${password}`, {
                method: "POST",
                headers: {
                    "Content-type": "application/json; charset=UTF-8"
                },
            });
            if (response.ok) {
                this.$router.push(this.$route.query.redirect || '/Login');
            }
        },
    }
};
</script>