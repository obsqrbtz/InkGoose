<script setup>
import NoteCard from './NoteCard.vue'
import NoteCreate from './NoteCreate.vue'
import Navbar from './Navbar.vue'
import { defineComponent } from 'vue';

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
        <div class="drawer-content flex flex-col items-center bg-base-100">
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
            <ul class="menu bg-base-300 text-base-content min-h-full w-60 p-4">
                <li>
                    <a @click="showModal = true">
                        Add
                    </a>
                </li>
                <li><a @click="showArchived = false">All</a></li>
                <li><a @click="showArchived = true">Archive</a></li>
            </ul>
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
            notesPinned: null,
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
        }
    }
})
</script>