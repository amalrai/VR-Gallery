<template>
  <a-card :bordered="false">
    <!-- 查询区域 -->
    <div class="table-page-search-wrapper">
      <a-form layout="inline" @keyup.enter.native="searchQuery">
        <a-row :gutter="24">
          <a-col :md="6" :sm="12">
            <a-form-item :label="$t('展品名')">
              <j-input :placeholder="$t('输入展品名模糊查询')" v-model="queryParam.name"></j-input>
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="12">
            <a-form-item :label="$t('编号')">
              <j-input :placeholder="$t('输入编号查询')" v-model="queryParam.exhibitsNo" type=""></j-input>
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="12">
            <a-form-item :label="$t('状态')">
              <a-select :placeholder="$t('选择状态查询')" v-model="queryParam.status" >
                          <a-select-option value="">{{ $t('请选择') }}</a-select-option>
                          <a-select-option value="0">{{ $t('启用') }}</a-select-option>
                          <a-select-option value="1">{{ $t('禁用') }}</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
            <a-col :md="6" :sm="12">
            <a-form-item :label="$t('权限')">
              <a-select :placeholder="$t('选择权限查询')" v-model="queryParam.auth" >
                                        <a-select-option value="">{{ $t('请选择') }}</a-select-option>
                                        <a-select-option value="0">{{ $t('免费') }}</a-select-option>
                                        <a-select-option value="1">{{ $t('VIP') }}</a-select-option>
                            </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="8">
                      <span style="float: left;overflow: hidden;" class="table-page-search-submitButtons">
                        <a-button type="primary" @click="searchQuery" icon="search">{{ $t('查询') }}</a-button>
                        <a-button type="primary" @click="searchReset" icon="reload" style="margin-left: 8px">{{ $t('重置') }}</a-button>
                      </span>
                    </a-col>
        </a-row>
      </a-form>
    </div>
    <!-- 查询区域-END -->

    <!-- 操作按钮区域 -->
    <div class="table-operator">
    </div>

    <!-- table区域-begin -->
    <div>
      <div class="ant-alert ant-alert-info" style="margin-bottom: 16px;">
        <i class="anticon anticon-info-circle ant-alert-icon"></i> {{ $t('已选择') }} <a style="font-weight: 600">{{ selectedRowKeys.length }}</a>{{ $t('项') }}
        <a style="margin-left: 24px" @click="onClearSelected">{{ $t('清空') }}</a>
      </div>

      <a-table
        ref="table"
        size="middle"
        :scroll="{x:true}"
        bordered
        rowKey="id"
        :columns="columns"
        :dataSource="dataSource"
        :pagination="ipagination"
        :loading="loading"
        :rowSelection="{selectedRowKeys: selectedRowKeys, onChange: onSelectChange}"
        class="j-table-force-nowrap"
        @change="handleTableChange">

        <template slot="htmlSlot" slot-scope="text">
          <div v-html="text"></div>
        </template>
        <template slot="imgSlot" slot-scope="text">
          <span v-if="!text" style="font-size: 12px;font-style: italic;">无图片</span>
          <img v-else :src="getImgView(text)" height="25px" alt="" style="max-width:80px;font-size: 12px;font-style: italic;"/>
        </template>
        <template slot="fileSlot" slot-scope="text">
          <span v-if="!text" style="font-size: 12px;font-style: italic;">无文件</span>
          <a-button
            v-else
            :ghost="true"
            type="primary"
            icon="download"
            size="small"
            @click="downloadFile(text)">
            下载
          </a-button>
        </template>


        <template slot="customRenderStatus" slot-scope="status">
                  <a-tag v-if="status==0" color="green">{{ $t('启用') }}</a-tag>
                  <a-tag v-if="status==1" color="red">{{ $t('禁用') }}</a-tag>
        </template>
        <template slot="customRenderAuth" slot-scope="auth">
                                  <a-tag v-if="auth==0" color="green">{{ $t('免费') }}</a-tag>
                                  <a-tag v-if="auth==1" color="orange">{{ $t('VIP') }}</a-tag>
                </template>
      </a-table>
    </div>

  </a-card>
</template>

<script>

  import '@/assets/less/TableExpand.less'
  import { mixinDevice } from '@/utils/mixin'
  import { JeecgListMixin } from '@/mixins/JeecgListMixin'

  import {deleteAction, httpAction} from "@api/manage";
  export default {
    name: 'ShowExhibitsListForm',
    mixins:[JeecgListMixin, mixinDevice],
    components: {
    },
    data () {
      return {
        description: '展品管理管理页面',
        //待交换的展品ID
        exhibitsId:'',
        roomId:'',
        // 表头
        columns: [
          {
            title: '#',
            dataIndex: '',
            key:'rowIndex',
            width:60,
            align:"center",
            customRender:function (t,r,index) {
              return parseInt(index)+1;
            }
          },
          {
            title:this.$t('编号'),
            align:"center",
            dataIndex: 'exhibitsNo'
          },
          {
            title:this.$t('展品名'),
            align:"center",
            dataIndex: 'name'
          },
          {
            title:this.$t('文字简介'),
            align:"center",
            dataIndex: 'text'
          },
          {
            title:this.$t('状态'),
            align:"center",
            dataIndex: 'status',
            scopedSlots: { customRender: 'customRenderStatus' },
            filterMultiple: false
          },
          {
            title:this.$t('权限'),
            align:"center",
            dataIndex: 'auth',
            scopedSlots: { customRender: 'customRenderAuth' }
          }
        ],
        /* 排序参数 */
        isorter:{
          column: 'exhibitsNo',
          order: 'asc',
        },
        url: {
          list: "/exhibits/showExhibits/list",
          replace: "/exhibits/showExhibits/replace"
        },
        disableMixinCreated:true,
        dictOptions:{},
        superFieldList:[],
      }
    },
    created() {
    this.getSuperFieldList();
    },
    computed: {
    },
    methods: {
      initDictConfig(){
      },
      getSuperFieldList(){},
      open(record){
        this.exhibitsId = record.id;
        this.roomId = record.showRoomId;
        this.queryParam.showRoomId = record.showRoomId;
        this.searchQuery();
      },
      searchReset() {
        this.queryParam = {}
        this.queryParam.showRoomId = this.roomId;
        this.searchQuery();
      },
      doReplace: function () {
        if (this.selectedRowKeys.length <= 0 || this.selectedRowKeys.length > 1) {
          this.$message.warning(this.$t('请选择一条记录！'));
          return;
        } else {
          var replaceExhibitsId = this.selectedRowKeys[0];
          if(this.exhibitsId == replaceExhibitsId){
            this.$message.warning(this.$t('请选择其他数据！'));
            return;
          }
          var that = this;
          this.$confirm({
            title: this.$t('确认替换'),
            content: this.$t('是否替换选中数据?'),
            okText:this.$t('确定'),
            cancelText:this.$t('取消'),
            onOk: function () {
              that.confirmLoading = true;
              deleteAction(that.url.replace, {exhibitsId: that.exhibitsId, replaceExhibitsId:replaceExhibitsId}).then((res) => {
                if(res.success){
                  that.$message.success(res.message);
                  that.$emit('ok');
                }else{
                  that.$message.warning(res.message);
                }
              }).finally(() => {
                that.confirmLoading = false;
              });
            }
          });
        }
      },
    }
  }
</script>
<style scoped>
  @import '~@assets/less/common.less';
</style>