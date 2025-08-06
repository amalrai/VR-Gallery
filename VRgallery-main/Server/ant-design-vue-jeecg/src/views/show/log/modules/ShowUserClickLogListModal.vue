<template>
  <j-modal
    :title="title"
    :width="width"
    :visible="visible"
    @ok="handleCancel"
    :okButtonProps="{ class:{'jee-hidden': disableSubmit} }"
    @cancel="handleCancel"
    >
    <show-user-click-log-list ref="realForm" :disabled="disableSubmit"></show-user-click-log-list>
  </j-modal>
</template>

<script>

  import ShowUserClickLogList from '../ShowUserClickLogList'
  export default {
    name: 'ShowUserClickLogListModal',
    components: {
      ShowUserClickLogList
    },
    data () {
      return {
        title:'',
        width:1200,
        visible: false,
        disableSubmit: false
      }
    },
    methods: {
      open (record) {
        this.visible=true
        this.$nextTick(()=>{
          this.$refs.realForm.open(record);
        })
      },
      close () {
        this.$emit('close');
        this.visible = false;
      },
      handleCancel () {
        this.close()
      }
    }
  }
</script>