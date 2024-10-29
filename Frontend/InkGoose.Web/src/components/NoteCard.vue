<script setup>

import markdownit from 'markdown-it'
const markdown = markdownit({
    html: true,
    linkify: true,
    typographer: true
});

// eslint-disable-next-line no-unused-vars
const props = defineProps({
    id: {
        type: String,
        required: true
    },
    archived: {
        type: Boolean,
        default: false
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
    <NoteEdit
        :id="id"
        :is-open="showModal"
        :title="title"
        :archived="archived"
        :note-content="noteContent"
        @update:is-open="showModal = $event"
        @notes-updated="reportUpdate"
    />
    <div
        class="w-full h-64 flex flex-col justify-between bg-base-100  border border-base-300 rounded-lg mb-6 pt-4 pb-1 px-4"
    >
        <div class="overflow-y-auto prose prose-sm">
            <h2>{{ title }}</h2>
            <div v-html="markdown.render(noteContent)" />
        </div>
        <div>
            <div class=" flex items-center justify-between text-base-content">
                <p class="text-sm">{{ new Date(dateCreated).toLocaleString() }}</p>
                <div class="flex mb-2 mt-2">
                    <button
                        class="btn btn-sm btn-circle mr-2"
                        aria-label="edit note"
                        role="button"
                        @click="showModal = true"
                    >
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            class="icon icon-tabler icon-tabler-pencil"
                            width="20"
                            height="20"
                            viewBox="0 0 24 24"
                            stroke-width="1.5"
                            stroke="currentColor"
                            fill="none"
                            stroke-linecap="round"
                            stroke-linejoin="round"
                        >
                            <path
                                stroke="none"
                                d="M0 0h24v24H0z"
                            />
                            <path d="M4 20h4l10.5 -10.5a1.5 1.5 0 0 0 -4 -4l-10.5 10.5v4" />
                            <line
                                x1="13.5"
                                y1="6.5"
                                x2="17.5"
                                y2="10.5"
                            />
                        </svg>
                    </button>
                    <!-- <button
                        class="btn btn-sm btn-circle"
                        aria-label="archive toggle"
                        role="button"
                        @click="toggleArchive()"
                    >
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            width="20"
                            height="20"
                            viewBox="0 0 24 24"
                            fill="none"
                            stroke="currentColor"
                            stroke-width="1.5"
                            stroke-linecap="round"
                            stroke-linejoin="round"
                        >
                            <path d="M21.5 12H16c-.7 2-2 3-4 3s-3.3-1-4-3H2.5" />
                            <path
                                d="M5.5 5.1L2 12v6c0 1.1.9 2 2 2h16a2 2 0 002-2v-6l-3.4-6.9A2 2 0 0016.8 4H7.2a2 2 0 00-1.8 1.1z"
                            />
                        </svg>
                    </button> -->
                    <button
                        class="btn btn-sm btn-circle"
                        aria-label="delete note"
                        role="button"
                        @click="deleteNote(id)"
                    >
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            width="20"
                            height="20"
                            viewBox="0 0 24 24"
                            fill="none"
                            stroke="currentColor"
                            stroke-width="1.5"
                            stroke-linecap="round"
                            stroke-linejoin="round"
                        >
                            <polyline points="3 6 5 6 21 6" />
                            <path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2" />
                            <line
                                x1="10"
                                y1="11"
                                x2="10"
                                y2="17"
                            />
                            <line
                                x1="14"
                                y1="11"
                                x2="14"
                                y2="17"
                            />
                        </svg>
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import NoteEdit from './NoteEdit.vue'
export default {
    components: {
        NoteEdit
    },
    emits: {
        notesUpdated: null
    },
    data() {
        return {
            showModal: false,
            editArchived: this.archived
        };
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
        async toggleArchive() {
            var params = {
                id: this.id,
                title: this.title,
                content: this.content,
                archived: !this.archive
            };
            var reqBody = JSON.stringify(params);
            const response = await fetch(`${this.apiHost}/Notes/UpdateNote`, {
                method: "PATCH",
                headers: {
                    "Content-type": "application/json; charset=UTF-8",
                    "Authorization": `Bearer ${window.localStorage.getItem("accessToken")}`
                },
                body: reqBody
            });
            if (response.status === 401) {
                window.localStorage.removeItem("accessToken");
                this.$router.push(this.$route.query.redirect || '/Login')
                return;
            }
            this.$emit('notesUpdated');;
        },
        reportUpdate() {
            this.$emit('notesUpdated');
        }
    },
}
</script>