<script setup>
import NoteCard from './NoteCard.vue'
import NoteCreate from './NoteCreate.vue'
</script>

<template>
    <NoteCreate :isOpen="showModal" @update:isOpen="showModal = $event" v-on:notesUpdated="fetchNotes" :id="id"
        :title="title" :noteContent="noteContent" />
    <div class="flex">
        <div class="basis-1/8">
            <button @click="showModal = true"
                class="sticky top-12 ml-4 w-8 h-8 rounded-full bg-gray-800 dark:bg-gray-100 dark:text-gray-800 text-white flex items-center justify-center focus:outline-none focus:ring-2 focus:ring-offset-2  focus:ring-black">
                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none"
                    stroke="#ffffff" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                    <line x1="12" y1="5" x2="12" y2="19"></line>
                    <line x1="5" y1="12" x2="19" y2="12"></line>
                </svg>
            </button>
        </div>
        <div v-if="data" class="container px-6">
            <div class="grid sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
                <NoteCard v-for="item in data" v-on:notesUpdated="fetchNotes" :id="item.id" :title="item.title"
                    :noteContent="item.content" :dateCreated="item.dateCreated" />
            </div>
        </div>
    </div>
</template>
<script>
import { ref } from 'vue';
export default {
    created() {
        this.fetchNotes();
    },
    data() {
        return {
            data: null,
            showModal: false
        };
    },
    components: {
        NoteCard,
        NoteCreate
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
        }
    }
};
</script>