<script setup>
import NoteCard from './NoteCard.vue'
</script>

<template>
    <button @click="createNote" class="btn btn-neutral m-4">Create note</button>
    <div v-if="data" class="grid grid-cols-3">
        <NoteCard v-for="item in data" :id="item.id" :title="item.title" :noteContent="item.content" />
    </div>
</template>
<script>
export default {
    created() {
        this.fetchNotes();
    },
    data() {
        return {
            data: null,
        };
    },
    components: {
        NoteCard
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
        }
    }
};
</script>