<template>
    <teleport to="body">
        <div class="absolute inset-0 overflow-y-auto bg-black bg-opacity-50" v-if="isOpen">
            <div class="flex items-start justify-center min-h-screen mt-24 text-center">
                <div class="bg-white text-black rounded-lg shadow-xl w-6/12 m-4">
                    <slot></slot>
                    <div class="flex items-end w-full justify-end p-2">
                        <button @click="close" class="btn btn-sm btn-circle btn-ghost">✕</button>
                    </div>
                    <div class="p-2">
                        <input @input="updateTitle($event.target.value)" type="text" :value="title" placeholder="Title"
                            class="input w-full focus:border-none focus:outline-none font-bold" />
                    </div>
                    <div>
                        <textarea @input="updateContent($event.target.value)"
                            class="textarea textarea-ghost textarea-lg whitespace-break-spaces w-full focus:border-none focus:outline-none"
                            :value="noteContent" placeholder="Content"></textarea>
                    </div>
                    <div class="flex items-center justify-end p-2">
                        <button @click="saveNote()" class="btn btn-sm btn-neutral">Save</button>
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
    emits: {
        notesUpdated: null,
        "update:isOpen": null
    },
    methods: {
        close() {
            this.$emit('update:isOpen', false);
        },
        async saveNote() {
            var requestParams = "";
            if (editTitle != undefined) {
                requestParams += `&title=${editTitle}`;
            }
            if (editContent != undefined) {
                requestParams += `&content=${editContent}`;
            }
            const response = await fetch(`${this.apiHost}/Notes/UpdateNote?id=${this.id}${requestParams}`, {
                method: "PATCH",
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