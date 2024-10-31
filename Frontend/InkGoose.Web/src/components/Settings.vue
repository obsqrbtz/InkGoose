<script setup>
import Navbar from './Navbar.vue'
</script>

<template>
    <Navbar />
    <div class="bg-base-100 w-full flex flex-row px-3">
        <main class="w-full min-h-screen py-1">
            <div class="w-full px-6 pb-8 mt-8">
                <h2 class="pl-6 text-2xl font-bold">Settings</h2>
                <div class="grid max-w-2xl mx-auto mt-8">
                    <div class="items-center mt-8">
                        <div class="flex flex-col items-center w-full mb-2 space-x-0 space-y-2">
                            <div class="w-full">
                                <label
                                    for="username"
                                    class="input input-bordered flex items-center gap-2"
                                >
                                    <svg
                                        xmlns="http://www.w3.org/2000/svg"
                                        viewBox="0 0 16 16"
                                        fill="currentColor"
                                        class="h-4 w-4 opacity-70"
                                    >
                                        <path
                                            d="M8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6ZM12.735 14c.618 0 1.093-.561.872-1.139a6.002 6.002 0 0 0-11.215 0c-.22.578.254 1.139.872 1.139h9.47Z"
                                        />
                                    </svg>
                                    <input
                                        id="username"
                                        type="text"
                                        class="grow"
                                        placeholder="Username"
                                        :value="user.userName"
                                    >

                                </label>
                            </div>
                        </div>

                        <div class="mb-2">
                            <label
                                for="email"
                                class="input input-bordered flex items-center gap-2"
                            >
                                <svg
                                    xmlns="http://www.w3.org/2000/svg"
                                    viewBox="0 0 16 16"
                                    fill="currentColor"
                                    class="h-4 w-4 opacity-70"
                                >
                                    <path
                                        d="M2.5 3A1.5 1.5 0 0 0 1 4.5v.793c.026.009.051.02.076.032L7.674 8.51c.206.1.446.1.652 0l6.598-3.185A.755.755 0 0 1 15 5.293V4.5A1.5 1.5 0 0 0 13.5 3h-11Z"
                                    />
                                    <path
                                        d="M15 6.954 8.978 9.86a2.25 2.25 0 0 1-1.956 0L1 6.954V11.5A1.5 1.5 0 0 0 2.5 13h11a1.5 1.5 0 0 0 1.5-1.5V6.954Z"
                                    />
                                </svg>
                                <input
                                    id="email"
                                    type="text"
                                    class="grow"
                                    placeholder="Email"
                                    :value="user.email"
                                >
                            </label>
                        </div>

                        <div class="mb-2">
                            <label
                                for="password"
                                class="input input-bordered flex items-center gap-2"
                            >
                                <svg
                                    xmlns="http://www.w3.org/2000/svg"
                                    viewBox="0 0 16 16"
                                    fill="currentColor"
                                    class="h-4 w-4 opacity-70"
                                >
                                    <path
                                        fill-rule="evenodd"
                                        d="M14 6a4 4 0 0 1-4.899 3.899l-1.955 1.955a.5.5 0 0 1-.353.146H5v1.5a.5.5 0 0 1-.5.5h-2a.5.5 0 0 1-.5-.5v-2.293a.5.5 0 0 1 .146-.353l3.955-3.955A4 4 0 1 1 14 6Zm-4-2a.75.75 0 0 0 0 1.5.5.5 0 0 1 .5.5.75.75 0 0 0 1.5 0 2 2 0 0 0-2-2Z"
                                        clip-rule="evenodd"
                                    />
                                </svg>
                                <input
                                    id="password"
                                    type="password"
                                    class="grow"
                                    :value="password"
                                >
                            </label>
                        </div>

                        <div class="flex justify-center mt-4">
                            <button
                                type="submit"
                                class="btn px-5 py-2.5"
                                @click="saveChanges()"
                            >
                                Save
                            </button>
                            <button
                                type="submit"
                                class="btn btn-error px-5 py-2.5 ml-2"
                                @click="deleteAccount()"
                            >
                                Delete account
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </main>
    </div>
</template>

<script>
var password;
export default {
    data() {
        return {
            user: null,
            password: null,
        };
    },
    async created() {
        const userInfo = await fetch(`${this.apiHost}/Users/GetUser`, {
            method: "GET",
            headers: {
                "Content-type": "application/json; charset=UTF-8",
                "Authorization": `Bearer ${window.localStorage.getItem("accessToken")}`
            },
        });
        if (!userInfo.ok) {
            alert("Failed to fetch user information. Please try again.");
            return;
        }
        this.user = await userInfo.json();
    },
    methods: {
        async saveChanges() {
            const updatedUser = {
                id: this.user.id,
                userName: this.user.userName,
                email: this.user.email,
                password: this.password, // Assuming the password is stored securely in a variable
            };
            const response = await fetch(`${this.apiHost}/Users/UpdateUser`, {
                method: "PUT",
                headers: {
                    "Content-type": "application/json; charset=UTF-8",
                    "Authorization": `Bearer ${window.localStorage.getItem("accessToken")}`
                },
                body: JSON.stringify(updatedUser),
            });
            if (!response.ok) {
                alert("Failed to save changes. Please try again.");
                return;
            }
            alert("Changes saved successfully.");
        },
        async deleteAccount() {
            const confirmation = confirm("Are you sure you want to delete your account?");
            if (!confirmation) {
                return;
            }
            const response = await fetch(`${this.apiHost}/Users/DeleteUser`, {
                method: "DELETE",
                headers: {
                    "Content-type": "application/json; charset=UTF-8",
                    "Authorization": `Bearer ${window.localStorage.getItem("accessToken")}`
                },
            });
            if (!response.ok) {
                alert("Failed to delete account. Please try again.");
                return;
            }
            alert("Account deleted successfully. You will be logged out.");
            window.localStorage.removeItem("accessToken");
            this.$router.push("/login");
        }              
    }
};
</script>