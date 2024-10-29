<script setup>
import NoteCard from './NoteCard.vue'
import NoteCreate from './NoteCreate.vue'

import { defineComponent } from 'vue';

</script>

<template>
    <NoteCreate
        :is-open="showModal"
        @update:is-open="showModal = $event"
        @notes-updated="fetchNotes"
    />
    <div class="flex">
        <div class="flex flex-col basis-1/8 sticky top-12 left-2">
            <button
                class="btn btn-sm my-1"
                @click="showModal = true"
            >
                Add
            </button>
            <button
                class="btn btn-sm my-1"
                @click="showArchived = false"
            >
                All
            </button>
            <button
                class="btn btn-sm my-1"
                @click="showArchived = true"
            >
                Archive
            </button>
        </div>
        <div
            v-if="notes && !showArchived"
            class="container px-6"
        >
            <div class="grid sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
                <NoteCard
                    v-for="item in notes"
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
        <div
            v-if="notesArchived && showArchived"
            class="container px-6"
        >
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
</template>

<script>
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
            showModal: false,
            showArchived: false,
        };
    },
    created() {
        this.fetchNotes();
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
                return item.archived === false;
            });
            this.notesArchived = this.data.filter(function (item) {
                return item.archived === true;
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
        }
    }
})
</script>