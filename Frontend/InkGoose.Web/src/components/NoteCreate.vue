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
        <div class="fixed inset-0 overflow-y-auto bg-base-100  bg-opacity-50" v-if="isOpen">
            <div class="flex items-start justify-center h-4/5 mt-24 text-center">
                <div class="bg-base-100  text-base-content border border-base-300 rounded-lg w-6/12 h-full m-4">
                    <slot></slot>
                    <div class="flex w-full p-2">
                        <div class="flex w-full justify-start ml-2 mt-2">
                            <button @click="createNote()" class="btn btn-sm btn-neutral">Save</button>
                        </div>
                        <div class="flex w-full justify-end">
                            <button @click="close" class="btn btn-sm btn-circle btn-ghost">✕</button>
                        </div>
                    </div>
                    <NoteView :id="id" :title="editTitle" :noteContent="editContent" @titleUpdated="updateTitle"
                        @contentUpdated="updateContent" />
                </div>
            </div>
        </div>
    </teleport>
</template>

<script>
export default {
    components: {
        NoteView
    },
    props: {
        isOpen: {
            type: Boolean,
            default: false
        }
    },
    data() {
        return {
            editTitle: '',
            editContent: ''
        }
    },
    emits: {
        notesUpdated: null,
        "update:isOpen": null
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
        async createNote() {
            var params = {
                id: this.id,
                title: this.editTitle,
                content: this.editContent
            };
            var reqBody = JSON.stringify(params);
            const response = await fetch(`${this.apiHost}/Notes/AddNote`, {
                method: "POST",
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
        }
    }
};
</script>