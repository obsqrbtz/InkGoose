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
    pinned:{
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
        class="w-full h-96 flex flex-col indicator
        justify-between bg-base-100  border border-base-300 
        rounded-lg mb-6 pt-4 pb-1 px-4
        hover:border-base-300 hover:shadow-md transition-all cursor-pointer"
    >
        <spam
            class="indicator-item indicator-end badge hover:badge-neutral z-0"
            @click="deleteNote(id)"
        >
            X
        </spam>
        <span
            v-if="editPinned"
            class="indicator-item indicator-center badge hover:badge-neutral z-0"
            @click="togglePinned()"
        > unpin
        </span>
        <span
            v-if="!editPinned"
            class="indicator-item indicator-center badge hover:badge-neutral z-0"
            @click="togglePinned()"
        > pin </span>
        <div
            class="p-4 overflow-y-auto prose prose-sm"
            @click="showModal = true"
        >
            <h5 class="mb-2 text-xl font-semibold">
                {{ title }}
            </h5>
            <div v-html="markdown.render(noteContent)" />
        </div>
        <div>
            <div class="flex flex-row items-start justify-content-start text-base-content">
                <!-- <p class="text-xs">{{ new Date(dateCreated).toLocaleString() }}</p> -->
                <div class="flex flex-row w-full justify-end pt-1 mb-1">
                    <span
                        class="inline-block bg-base-300 rounded-full px-2 py-1 text-xs font-semibold mr-1"
                    >#photography</span>
                    <span
                        class="inline-block bg-base-300 rounded-full px-2 py-1 text-xs font-semibold mr-1"
                    >#travel</span>
                    <span class="inline-block bg-base-300 rounded-full px-2 py-1 text-xs font-semibold">+2</span>
                </div>
            </div>
            <!-- <div class="flex flex-row mb-2 mt-2 justify-end" /> bottom button block -->
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
            editArchived: this.archived,
            editPinned: this.pinned
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
        async togglePinned(){
            var params = {
                id: this.id,
                title: this.title,
                content: this.content,
                tag: this.tag,
                color: this.color,
                archived: this.archive,
                pinned: !this.pinned
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