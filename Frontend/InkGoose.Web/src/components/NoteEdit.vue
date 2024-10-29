<script setup>
import NoteView from './NoteView.vue'
</script>
<template>
    <teleport to="body">
        <div
            v-if="isOpen"
            class="fixed inset-0 overflow-y-auto bg-base-100 bg-opacity-50"
        >
            <div class="flex justify-center m-4">
                <div
                    class="flex flex-col bg-base-100 text-base-content border border-base-300 rounded-lg w-full lg:w-6/12 m-4"
                >
                    <slot />
                    <NoteView
                        :id="id"
                        :title="title"
                        :note-content="noteContent"
                        class=""
                        @title-updated="updateTitle"
                        @content-updated="updateContent"
                    />
                    <div class="w-full justify-items-start p-2 ml-2">
                        <button
                            class="btn btn-sm"
                            @click="saveNote()"
                        >
                            Save
                        </button>
                        <button
                            class="btn btn-sm ml-1"
                            @click="updateArcStatus"
                        >
                            {{ archivedStr }}
                        </button>
                        <button
                            class="btn btn-sm ml-1"
                            @click="close"
                        >
                            Close
                        </button>
                    </div>
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
        }
    },
    emits: {
        notesUpdated: null,
        "update:isOpen": null
    },
    data() {
        return {
            editTitle: this.title,
            editContent: this.noteContent,
            editArchived: this.edit,
            archivedStr: this.archived ? "Unarchive" : "Archive"
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
        updateArcStatus() {
            this.editArchived = !this.editArchived;
            this.archivedStr = this.editArchived ? "Unarchive" : "Archive";
        },
        async saveNote() {
            var params = {
                id: this.id,
                title: this.editTitle,
                content: this.editContent,
                archived: this.editArchived
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