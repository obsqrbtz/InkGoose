<script setup>
const props = defineProps({
    id: {
        type: String,
        required: true
    },
    title: {
        type: String,
        required: true
    },
    noteContent: {
        type: String,
        required: true
    }
});

</script>

<template>
    <!-- <div class="card bg-base-100 w-96 shadow-md m-4">
        <div class="card-body">
            <h2 class="card-title">
                {{ title }}
            </h2>
            <p> {{ noteContent }} </p>
            <div class="grid grid-cols-2 h-8">
                <button @click="showModal = true" class="btn btn-outline btn-default mx-2">Edit</button>
                <NoteEdit :isOpen="showModal" @update:isOpen="showModal = $event" v-on:notesUpdated="reportUpdate"
                    :id="id" :title="title" :noteContent="noteContent" />
                <button @click="deleteNote(id)" class="btn btn-outline btn-default mx-2">Delete</button>
            </div>
        </div>
    </div> -->
    <NoteEdit :isOpen="showModal" @update:isOpen="showModal = $event" v-on:notesUpdated="reportUpdate" :id="id"
        :title="title" :noteContent="noteContent" />
    <div
        class="w-full h-64 flex flex-col justify-between dark:bg-gray-800 bg-white dark:border-gray-700 rounded-lg border border-gray-400 mb-6 py-5 px-4">
        <div>
            <h4 class="text-gray-800 dark:text-gray-100 font-bold mb-3">{{ title }}</h4>
            <p class="text-gray-800 dark:text-gray-100 text-sm">{{ noteContent }}</p>
        </div>
        <div>
            <div class="flex items-center justify-between text-gray-800 dark:text-gray-100">
                <p class="text-sm">March 28, 2020</p>
                <div class="flex">
                    <button @click="showModal = true"
                        class="mr-2 w-8 h-8 rounded-full bg-gray-800 dark:bg-gray-100 dark:text-gray-800 text-white flex items-center justify-center focus:outline-none focus:ring-2 focus:ring-offset-2  focus:ring-black"
                        aria-label="edit note" role="button">
                        <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-pencil" width="20"
                            height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" fill="none"
                            stroke-linecap="round" stroke-linejoin="round">
                            <path stroke="none" d="M0 0h24v24H0z"></path>
                            <path d="M4 20h4l10.5 -10.5a1.5 1.5 0 0 0 -4 -4l-10.5 10.5v4"></path>
                            <line x1="13.5" y1="6.5" x2="17.5" y2="10.5"></line>
                        </svg>
                    </button>
                    <button @click="deleteNote(id)"
                        class="w-8 h-8 rounded-full bg-gray-800 dark:bg-gray-100 dark:text-gray-800 text-white flex items-center justify-center focus:outline-none focus:ring-2 focus:ring-offset-2  focus:ring-black"
                        aria-label="edit note" role="button">
                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none"
                            stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round">
                            <polyline points="3 6 5 6 21 6"></polyline>
                            <path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2">
                            </path>
                            <line x1="10" y1="11" x2="10" y2="17"></line>
                            <line x1="14" y1="11" x2="14" y2="17"></line>
                        </svg>
                    </button>
                </div>
                <!-- <button @click="deleteNote(id)" class="btn btn-outline btn-default mx-2">Delete</button> -->
            </div>
        </div>
    </div>
</template>

<script>
import NoteEdit from './NoteEdit.vue'
import { ref } from 'vue';
const components = {
    NoteEdit,
}
export default {
    components: {
        NoteEdit
    },
    data() {
        return {
            showModal: false
        };
    },
    emits: {
        notesUpdated: null
    },
    methods: {
        async deleteNote(id) {
            const response = await fetch(`${this.apiHost}/DeleteNote?id=${id}`, {
                method: "DELETE",
                headers: {
                    "Content-type": "application/json; charset=UTF-8"
                },
            });
            //this.$router.go()
            this.$emit('notesUpdated', id);
        },
        reportUpdate() {
            this.$emit('notesUpdated');
        }
    },
}
</script>