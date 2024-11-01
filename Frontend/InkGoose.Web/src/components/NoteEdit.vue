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
                    <div class="flex flex-row w-full justify-start items-center pt-1 ml-4 mb-1 mt-1 overflow-x-auto">
                        <span
                            v-for="tag in tags"
                            :key="tag"
                            class="inline-block bg-base-300 rounded-full px-2 py-1 text-xs font-semibold mr-1"
                        >{{
                             tag.value
                         }}
                            <button
                                class="btn btn-xs btn-ghost btn-circle ml-1"
                                @click="deleteTag(tag)"
                            >X</button></span>
                        <span class="grow" />
                        <div class="join mr-8">
                            <input
                                v-model="newTag"
                                type="text"
                                placeholder="New tag"
                                class="input input-bordered input-xs join-item"
                            >
                            <button
                                class="btn btn-xs join-item"
                                @click="addTag"
                            >
                                Add
                            </button>
                        </div>
                    </div>
                    <div class="w-full justify-items-start p-2 ml-2 join-item">
                        <button
                            class="btn btn-sm"
                            @click="saveNote()"
                        >
                            Save
                        </button>
                        <button
                            class="btn btn-sm ml-1"
                            @click="updateArcStatus()"
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
            editArchived: this.archived,
            archivedStr: this.archived ? "Unarchive" : "Archive",
            newTag: null,
            tags: null
        }
    },
    created() {
        this.fetchTags();
    },
    methods: {
        close() {
            this.$emit('notesUpdated');
            this.$emit('update:isOpen', false);
        },
        updateTitle(newTitle) {
            this.editTitle = newTitle;
        },
        updateContent(newContent) {
            this.editContent = newContent;
        },
        async fetchTags() {
            const response = await fetch(`${this.apiHost}/Notes/GetTags?noteid=${this.id}`, {
                method: "GET",
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
            this.tags = await response.json();
        },
        async addTag() {
            const response = await fetch(`${this.apiHost}/Notes/AddTag?noteid=${this.id}&value=${this.newTag}`, {
                method: "PATCH",
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
            this.fetchTags();
        },
        async deleteTag(tag) {
            const response = await fetch(`${this.apiHost}/Notes/DeleteTag?noteid=${this.id}&tagid=${tag.id}`, {
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
            this.fetchTags();
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
        async updateArcStatus() {
            this.editArchived = !this.editArchived;
            this.archivedStr = this.editArchived ? "Unarchive" : "Archive";
            await this.saveNote();
        },
    }
};
</script>