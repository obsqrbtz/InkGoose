<script setup lang="ts">

import markdownit from 'markdown-it'
const markdown = markdownit({
    html: true,
    linkify: true,
    typographer: true
});

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
    },
    dateCreated: {
        type: Date,
        required: true
    }
});

</script>

<template>
    <NoteEdit :isOpen="showModal" @update:isOpen="showModal = $event" v-on:notesUpdated="reportUpdate" :id="id"
        :title="title" :noteContent="noteContent" />
    <div
        class="w-full h-64 flex flex-col justify-between bg-base border border-base-300 rounded-lg mb-6 py-4 px-4">
        <div class="overflow-y-auto prose prose-sm">
            <h2>{{ title }}</h2>
            <div v-html="markdown.render(noteContent)" />
        </div>
        <div>
            <div class=" flex items-center justify-between text-base-content">
                <p class="text-sm">{{ new Date(dateCreated).toLocaleString() }}</p>
                <div class="flex">
                    <button @click="showModal = true"
                        class="mr-2 w-8 h-8 rounded-full bg-neutral text-neutral-content flex items-center justify-center focus:outline-none focus:ring-2 focus:ring-offset-2  focus:ring-black"
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
                        class="w-8 h-8 rounded-full bg-neutral text-neutral-content flex items-center justify-center focus:outline-none focus:ring-2 focus:ring-offset-2  focus:ring-black"
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
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import NoteEdit from './NoteEdit.vue'
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
            const response = await fetch(`${this.apiHost}/Notes/DeleteNote?id=${id}`, {
                method: "DELETE",
                headers: {
                    "Content-type": "application/json; charset=UTF-8",
                    "Authorization": `Bearer ${window.localStorage.getItem("accessToken")}`
                },
            });
            if (response.status === 401) {
                window.localStorage.removeItem("accessToken");
                this.$router.push(this.$route.query.redirect || '/Login')
                return;
            }
            this.$emit('notesUpdated');
        },
        reportUpdate() {
            this.$emit('notesUpdated');
        }
    },
}
</script>