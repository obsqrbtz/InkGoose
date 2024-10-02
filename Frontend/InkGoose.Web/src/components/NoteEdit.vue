<script setup>
import markdownit from 'markdown-it'
const markdown = markdownit({
    html: true,
    linkify: true,
    typographer: true
});
</script>
<template>
    <teleport to="body">
        <div class="fixed inset-0 overflow-y-auto bg-black bg-opacity-50" v-if="isOpen">
            <div class="flex items-start justify-center h-4/5 mt-24 text-center">
                <div class="bg-white text-black rounded-lg shadow-xl w-6/12 h-full m-4">
                    <slot></slot>
                    <div class="flex w-full p-2">
                        <div class="flex w-full justify-start">
                            <button @click="saveNote()" class="btn btn-sm btn-neutral">Save</button>
                        </div>
                        <div class="flex w-full justify-end">
                            <button @click="close" class="btn btn-sm btn-circle btn-ghost">✕</button>
                        </div>
                    </div>
                    <div class="flex w-full flex-col lg:flex-row h-full">
                        <div class="w-full">
                            <div class="p-2">
                                <input v-model="editTitle" type="text" placeholder="Title"
                                    class="input w-full focus:border-none focus:outline-none font-bold" />
                            </div>
                            <div class="h-3/4">
                                <textarea v-model="editContent"
                                    class="textarea textarea-ghost textarea-lg h-full whitespace-break-spaces w-full focus:border-none focus:outline-none"
                                    placeholder="Content"></textarea>
                            </div>
                        </div>
                        <div class="divider lg:divider-horizontal" />
                        <div class="w-full">
                            <div class="">
                                <div class="p-2">
                                    <h2> {{ editTitle }} </h2>
                                </div>
                                <div class="h-3/4">
                                    <div class="text-left" v-html="markdown.render(editContent)" />
                                </div>
                            </div>
                        </div>
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
        title: {
            type: String,
            required: true
        },
        noteContent: {
            type: String,
            required: true
        }
    },
    data() {
        return {
            editTitle: this.title,
            editContent: this.noteContent
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