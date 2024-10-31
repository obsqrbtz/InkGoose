<script setup>
import NoteCard from './NoteCard.vue'
import NoteCreate from './NoteCreate.vue'
import Navbar from './Navbar.vue'
import { defineComponent } from 'vue';

var accessToken = localStorage.getItem("accessToken");

</script>

<template>
    <NoteCreate
        :is-open="showModal"
        @update:is-open="showModal = $event"
        @notes-updated="fetchNotes"
    />
    <div class="drawer lg:drawer-open">
        <input
            id="my-drawer-2"
            type="checkbox"
            class="drawer-toggle"
        >
        <label
            for="my-drawer-2"
            class="btn mt-2 btn-ghost drawer-button lg:hidden"
        >
            <svg
                xmlns="http://www.w3.org/2000/svg"
                width="24"
                height="24"
                viewBox="0 0 24 24"
                fill="none"
                stroke="currentColor"
                stroke-width="2"
                stroke-linecap="round"
                stroke-linejoin="round"
            >
                <line
                    x1="3"
                    y1="12"
                    x2="21"
                    y2="12"
                />
                <line
                    x1="3"
                    y1="6"
                    x2="21"
                    y2="6"
                />
                <line
                    x1="3"
                    y1="18"
                    x2="21"
                    y2="18"
                />
            </svg>
        </label>
        <div class="drawer-content flex flex-col items-center lg:bg-base-100">
            <Navbar />
            <div
                v-if="notes && !showArchived"
                class="container px-6"
            >
                <div
                    v-if="notesPinned.length > 0"
                    class="text-2xl mb-2"
                >
                    Pinned
                </div>
                <div class="grid sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
                    <NoteCard
                        v-for="item in notesPinned"
                        :id="item.id"
                        :key="item.id"
                        :archived="item.archived"
                        :pinned="item.pinned"
                        :color="item.color"
                        :title="item.title"
                        :note-content="item.content"
                        :tag="item.tag"
                        :date-created="item.dateCreated"
                        @notes-updated="fetchNotes"
                    />
                </div>
                <div class="text-2xl mb-2">
                    All
                </div>
                <div class="grid sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
                    <NoteCard
                        v-for="item in notes"
                        :id="item.id"
                        :key="item.id"
                        :archived="item.archived"
                        :pinned="item.pinned"
                        :color="item.color"
                        :title="item.title"
                        :note-content="item.content"
                        :tag="item.tag"
                        :date-created="item.dateCreated"
                        @notes-updated="fetchNotes"
                    />
                </div>
            </div>
            <div
                v-if="notesArchived && showArchived"
                class="container px-6"
            >
                <div class="text-2xl mb-2">
                    Archive
                </div>
                <div class="grid sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
                    <NoteCard
                        v-for="item in notesArchived"
                        :id="item.id"
                        :key="item.id"
                        :archived="item.archived"
                        :title="item.title"
                        :note-content="item.content"
                        :date-created="item.dateCreated"
                        @notes-updated="fetchNotes"
                    />
                </div>
            </div>
        </div>
        <div class="drawer-side">
            <label
                for="my-drawer-2"
                aria-label="close sidebar"
                class="drawer-overlay"
            />
            <div class="bg-base-200 h-full">
                <div class="flex items-center justify-between px-8 py-5 bg-base-300">
                    <div class="flex items-center mr-5">
                        <div class="mr-2 ">
                            <a
                                href="javascript:void(0)"
                                class="hover:text-primary transition-colors duration-200 ease-in-out text-[1.075rem] font-medium text-content"
                            >{{
                                userName }} </a>
                            <span class="text-secondary-content font-medium block text-[0.85rem]"> {{ email }} </span>
                            <a
                                v-if="accessToken"
                                class="hover:text-primary transition-colors duration-200 ease-in-out text-[0.85rem] cursor-pointer"
                                @click="logout"
                            >
                                log out
                            </a>
                        </div>
                    </div>
                    <router-link
                        to="/settings"
                        class="inline-flex relative items-center group justify-end font-medium leading-normal
                        text-center align-middle cursor-pointer rounded-[.95rem] transition-colors duration-150
                        ease-in-out bg-transparent shadow-none border-0"
                        href="javascript:void(0)"
                    >
                        <span
                            class="leading-none transition-colors duration-200 ease-in-out peer shrink-0 group-hover:text-primary"
                        >
                            <svg
                                xmlns="http://www.w3.org/2000/svg"
                                fill="none"
                                viewBox="0 0 24 24"
                                stroke-width="1.5"
                                stroke="currentColor"
                                class="w-6 h-6"
                            >
                                <path
                                    stroke-linecap="round"
                                    stroke-linejoin="round"
                                    d="M9.594 3.94c.09-.542.56-.94 1.11-.94h2.593c.55 0 1.02.398 1.11.94l.213 1.281c.063.374.313.686.645.87.074.04.147.083.22.127.324.196.72.257 1.075.124l1.217-.456a1.125 1.125 0 011.37.49l1.296 2.247a1.125 1.125 0 01-.26 1.431l-1.003.827c-.293.24-.438.613-.431.992a6.759 6.759 0 010 .255c-.007.378.138.75.43.99l1.005.828c.424.35.534.954.26 1.43l-1.298 2.247a1.125 1.125 0 01-1.369.491l-1.217-.456c-.355-.133-.75-.072-1.076.124a6.57 6.57 0 01-.22.128c-.331.183-.581.495-.644.869l-.213 1.28c-.09.543-.56.941-1.11.941h-2.594c-.55 0-1.02-.398-1.11-.94l-.213-1.281c-.062-.374-.312-.686-.644-.87a6.52 6.52 0 01-.22-.127c-.325-.196-.72-.257-1.076-.124l-1.217.456a1.125 1.125 0 01-1.369-.49l-1.297-2.247a1.125 1.125 0 01.26-1.431l1.004-.827c.292-.24.437-.613.43-.992a6.932 6.932 0 010-.255c.007-.378-.138-.75-.43-.99l-1.004-.828a1.125 1.125 0 01-.26-1.43l1.297-2.247a1.125 1.125 0 011.37-.491l1.216.456c.356.133.751.072 1.076-.124.072-.044.146-.087.22-.128.332-.183.582-.495.644-.869l.214-1.281z"
                                />
                                <path
                                    stroke-linecap="round"
                                    stroke-linejoin="round"
                                    d="M15 12a3 3 0 11-6 0 3 3 0 016 0z"
                                />
                            </svg>
                        </span>
                    </router-link>
                </div>

                <div class="relative pl-3 my-5">
                    <div class="flex flex-col w-full font-medium">
                        <!-- menu item -->
                        <div>
                            <span
                                class="select-none flex items-center px-4 1 cursor-pointer my-[.4rem] rounded-[.95rem]"
                            >
                                <a
                                    class="flex items-center flex-grow text-[1.15rem] hover:text-primary"
                                    @click="showModal = true"
                                >New note</a>
                            </span>
                        </div>


                        <!-- menu item -->
                        <div>
                            <span
                                class="select-none flex items-center px-4 1 cursor-pointer my-[.4rem] rounded-[.95rem]"
                            >
                                <a
                                    class=" flex items-center flex-grow text-[1.15rem] hover:text-primary"
                                    @click="showArchived = false"
                                >All</a>
                            </span>
                        </div>

                        <!-- menu item -->
                        <div>
                            <span
                                class="select-none flex items-center px-4 py-1 cursor-pointer my-[.4rem] rounded-[.95rem]"
                            >
                                <a
                                    class="flex items-center flex-grow text-[1.15rem] hover:text-primary"
                                    @click="showArchived = true"
                                >Archive</a>
                            </span>
                        </div>

                        <!-- menu item -->
                        <div class="block pt-5 pb-[.15rem]">
                            <div class="px-4 py-1">
                                <span class="font-semibold text-[0.95rem] uppercase text-secondary-content">Tags</span>
                            </div>
                        </div>

                        <!-- menu item -->
                        <div
                            v-for="tag in tags"
                            :key="tag"
                            class="overflow-auto"
                        >
                            <span class="select-none flex items-center px-4 py-1 cursor-pointer">
                                <a class="flex items-center flex-grow text-[1.15rem] hover:text-primary"> {{ tag }}</a>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
var email;
var userName;
var tags = [
    "main",
    "personal",
    "work",
    "family",
    "hobbies",
    "shopping",
];
export default defineComponent({
    components: {
        NoteCard,
        NoteCreate
    },
    data() {
        return {
            data: null,
            notes: null,
            notesArchived: null,
            notesPinned: null,
            showModal: false,
            showArchived: false,
            tags: null,
        };
    },
    created() {
        this.fetchNotes();
        email = window.localStorage.getItem("email");
        userName = window.localStorage.getItem("userName");
    },
    methods: {
        async fetchNotes() {
            const response = await fetch(`${this.apiHost}/Notes/GetNotesList`, {
                method: "GET",
                headers: {
                    "Content-type": "application/json; charset=UTF-8",
                    "Authorization": `Bearer ${window.localStorage.getItem("accessToken")}`
                }
            });
            if (response.status === 401) {
                window.localStorage.removeItem("accessToken");
                this.$router.push(this.$route.query.redirect || '/Login')
                return;
            }
            this.data = await response.json();
            this.notes = this.data.filter(function (item) {
                return item.archived === false && item.pinned === false;
            });
            this.notesArchived = this.data.filter(function (item) {
                return item.archived === true;
            });
            this.notesPinned = this.data.filter(function (item) {
                return item.pinned === true;
            });
        },
        async createNote() {
            var title = "New note";
            var content = "Note was created successfully";
            const response = await fetch(`${this.apiHost}/Notes/AddNote?title=${title}&content=${content}`, {
                method: "POST",
                headers: {
                    "Content-type": "application/json; charset=UTF-8",
                    "Authorization": `Bearer ${window.localStorage.getItem("accessToken")}`
                }
            });
            if (response.status === 401) {
                window.localStorage.removeItem("accessToken");
                this.$router.push(this.$route.query.redirect || '/Login')
                return;
            }
            this.fetchNotes()
        },
        logout() {
            window.localStorage.removeItem("accessToken");
            window.localStorage.removeItem("email");
            window.localStorage.removeItem("userName");
            this.$router.push(this.$route.query.redirect || '/')
        }
    }
})
</script>