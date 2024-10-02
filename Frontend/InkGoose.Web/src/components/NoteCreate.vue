<template>
    <teleport to="body">
        <div class="absolute inset-0 overflow-y-auto bg-black bg-opacity-50" v-if="isOpen">
            <div class="flex items-start justify-center min-h-screen mt-24 text-center">
                <div class="bg-white text-black rounded-lg shadow-xl w-full max-w-xs">
                    <slot></slot>
                    <div class="flex items-end w-full justify-end p-2">
                        <button @click="close" class="btn btn-sm btn-circle btn-ghost">✕</button>
                    </div>
                    <div class="p-2">
                        <input @input="updateTitle($event.target.value)" type="text" placeholder="Title"
                            class="input w-full max-w-xs" />
                        <textarea @input="updateContent($event.target.value)"
                            class="textarea textarea-ghost textarea-lg w-full mt-2 max-w-xs"
                            placeholder="Content"></textarea>
                    </div>
                    <div class="flex items-center justify-end p-2">
                        <button @click="createNote" class="btn btn-sm btn-neutral">Save</button>
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
        async createNote() {
            var requestParams = "";
            if (editTitle != undefined) {
                requestParams += `&title=${editTitle}`;
            }
            if (editContent != undefined) {
                requestParams += `&content=${editContent}`;
            }
            const response = await fetch(`${this.apiHost}/Notes/AddNote?${requestParams}`, {
                method: "POST",
                headers: {
                    "Content-type": "application/json; charset=UTF-8",
                    "Authorization": `Bearer ${window.localStorage.getItem("accessToken")}`
                }
            });
            this.$emit('notesUpdated');
            this.$emit('update:isOpen', false);
        },
        updateTitle(value) {
            editTitle = value
        },
        updateContent(value) {
            editContent = value
        }
    }
};
var editTitle;
var editContent;
</script>