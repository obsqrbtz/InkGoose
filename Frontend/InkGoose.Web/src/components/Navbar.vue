<script setup>
import ThemeToggle from './ThemeToggle.vue';
</script>
<template>
    <div class="navbar bg-base-100 -100 flex sticky top-0">
        <div class="navbar-start basis-1/4">
            <router-link class="btn btn-ghost text-xl" to="/Notes">
                ink goose
            </router-link>
        </div>
        <div class="navbar-center basis-1/2">
            <!-- <label class="input input-bordered flex items-center gap-2 w-full">
                <input
                    type="text"
                    class="grow"
                    placeholder="Search"
                >
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 16 16"
                    fill="currentColor"
                    class="h-4 w-4 opacity-70"
                >
                    <path
                        fill-rule="evenodd"
                        d="M9.965 11.026a5 5 0 1 1 1.06-1.06l2.755 2.754a.75.75 0 1 1-1.06 1.06l-2.755-2.754ZM10.5 7a3.5 3.5 0 1 1-7 0 3.5 3.5 0 0 1 7 0Z"
                        clip-rule="evenodd"
                    />
                </svg>
            </label> -->
        </div>
        <div class="navbar-end basis-1/4">
            <div class="dropdown">
                <div tabindex="0" role="button" class="btn btn-ghost m-1"> {{ email }} </div>
                <ul tabindex="0" class="dropdown-content menu bg-base-100 rounded-box z-[1] w-52 p-2 shadow">
                    <li><a>Log out</a></li>
                </ul>
            </div>
            <ThemeToggle class="mr-2" />
        </div>
    </div>
</template>

<script>
// eslint-disable-next-line no-unused-vars
var email;
export default {
    created() {
        email = window.localStorage.getItem("email");
    }
}; 
</script>
