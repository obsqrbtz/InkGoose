<script setup>
import NoteCard from './NoteCard.vue'
import NoteCreate from './NoteCreate.vue'
</script>

<template>
    <!-- <button @click="createNote" class="btn btn-neutral m-4">Create note</button>
    <div v-if="data" class="grid grid-cols-3">
        <NoteCard v-for="item in data" v-on:notesUpdated="fetchNotes" :id="item.id" :title="item.title"
            :noteContent="item.content" />
    </div> -->

    <button @click="showModal = true" class="btn btn-neutral m-4 ml-24">New note</button>
    <NoteCreate :isOpen="showModal" @update:isOpen="showModal = $event" v-on:notesUpdated="fetchNotes" :id="id"
        :title="title" :noteContent="noteContent" />
    <div v-if="data" class="mx-auto container px-6">
        <div class="grid sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
            <NoteCard v-for="item in data" v-on:notesUpdated="fetchNotes" :id="item.id" :title="item.title"
                :noteContent="item.content" />
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
            const response = await fetch(`${this.apiHost}/GetNotesList`);
            this.data = await response.json();
        },
        async createNote() {
            var title = "New note";
            var content = "Note was created successfully";
            const response = await fetch(`${this.apiHost}/AddNote?title=${title}&content=${content}`, {
                method: "POST",
                headers: {
                    "Content-type": "application/json; charset=UTF-8"
                }
            });
            this.fetchNotes()
        }
    }
};
</script>