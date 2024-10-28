<script setup>
import markdownit from 'markdown-it'
import { defineComponent } from 'vue';

const markdown = markdownit({
    html: true,
    linkify: true,
    typographer: true
});
// eslint-disable-next-line no-unused-vars
const emit = defineEmits(['titleUpdated', 'contentUpdated'])
</script>
<template>
    <div role="tablist" class="tabs tabs-lifted items-start content-start bg-base-100 text-base-content m-1">
        <input type="radio" name="editorTabs" role="tab" class="tab" checked="checked" aria-label="Edit">
        <div role="tabpanel" class="h-[80vh] tab-content p-10">
            <input v-model="editTitle" type="text" placeholder="Title"
                class="input w-full bg-base-100 focus:border-none focus:outline-none font-bold"
                @input="$emit('titleUpdated', editTitle)">
            <textarea v-model="editContent"
                class="h-full pl-6 bg-base-100 resize-none leading-snug font-mono textarea textarea-ghost textarea-md whitespace-break-spaces w-full focus:border-none focus:outline-none"
                placeholder="Content" @input="$emit('contentUpdated', editContent)" />
        </div>
        <input type="radio" name="editorTabs" role="tab" class="tab" aria-label="Preview">
        <div role="tabpanel" class="tab-content p-10">
            <div class="h-[65vh] overflow-y-auto">
                <div class="text-left prose prose-md ml-2 mb-8">
                    <h1> {{ editTitle }} </h1>
                </div>
                <p class="prose prose-md text-left m-2" v-html="markdown.render(editContent)" />
            </div>
        </div>
        <span class="tab" />
    </div>
</template>

<script>
export default defineComponent({
    props: {
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
})
</script>