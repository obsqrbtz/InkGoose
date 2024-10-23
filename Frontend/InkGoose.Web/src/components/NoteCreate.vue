<script setup>
import NoteView from './NoteView.vue'
</script>
<template>
    <teleport to="body">
        <div
            v-if="isOpen"
            class="fixed inset-0 overflow-y-auto bg-base-100  bg-opacity-50"
        >
            <div class="flex items-start justify-center h-4/5 mt-24 text-center">
                <div class="bg-base-100  text-base-content border border-base-300 rounded-lg w-6/12 h-full m-4">
                    <slot />
                    <div class="flex w-full p-2">
                        <div class="flex w-full justify-start ml-2 mt-2">
                            <button
                                class="btn btn-sm btn-neutral"
                                @click="createNote()"
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
                        :title="editTitle"
                        :note-content="editContent"
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
    components: {
        NoteView
    },
    props: {
        isOpen: {
            type: Boolean,
            default: false
        }
    },
    emits: {
        notesUpdated: null,
        "update:isOpen": null
    },
    data() {
        return {
            editTitle: '',
            editContent: ''
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