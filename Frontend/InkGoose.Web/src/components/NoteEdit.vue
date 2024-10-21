<script setup>
import markdownit from 'markdown-it'
import NoteView from './NoteView.vue'
const markdown = markdownit({
    html: true,
    linkify: true,
    typographer: true
});
</script>
<template>
    <teleport to="body">
        <div
            v-if="isOpen"
            class="fixed inset-0 overflow-y-auto bg-base-100   bg-opacity-50"
        >
            <div class="flex items-start justify-center h-4/5 mt-24 text-center">
                <div class="bg-base-100  text-base-content border border-base-300 rounded-lg w-6/12 h-full m-4">
                    <slot />
                    <div class="flex w-full p-2">
                        <div class="flex w-full justify-start">
                            <button
                                class="btn btn-sm btn-neutral"
                                @click="saveNote()"
                            >
                                Save
                            </button>
                        </div>
                        <div class="flex w-full justify-end">
                            <button
                                class="btn btn-sm btn-circle btn-ghost"
                                @click="close"
                            >
                                ✕
                            </button>
                        </div>
                    </div>
                    <NoteView
                        :id="id"
                        :title="title"
                        :note-content="noteContent"
                        @title-updated="updateTitle"
                        @content-updated="updateContent"
                    />
                </div>
            </div>
        </div>
    </teleport>
</template>

<script>
export default {
    props: {
        isOpen: {
            type: Boolean,
            default: false
        },
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
    },
    emits: {
        notesUpdated: null,
        "update:isOpen": null
    },
    data() {
        return {
            editTitle: this.title,
            editContent: this.noteContent
        }
    },
    methods: {
        close() {
            this.$emit('update:isOpen', false);
        },
        updateTitle(newTitle) {
            this.editTitle = newTitle;
        },
        updateContent(newContent) {
            this.editContent = newContent;
        },
        async saveNote() {
            var params = {
                id: this.id,
                title: this.editTitle,
                content: this.editContent
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
            this.$emit('notesUpdated');
            this.$emit('update:isOpen', false);
        },
    }
};
</script>